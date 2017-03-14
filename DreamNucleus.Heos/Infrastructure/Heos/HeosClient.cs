using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos;
using DreamNucleus.Heos.Commands;
using DreamNucleus.Heos.Events;
using DreamNucleus.Heos.Infrastructure.Extensions;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Interfaces;
using Newtonsoft.Json;
using NLog;

namespace DreamNucleus.Heos.Infrastructure.Heos
{
    public class HeosClient
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly CancellationToken cancellationToken;
        private readonly IHeosTelnetClient heosTelnetClient;

        public static JsonSerializerSettings JsonSerializerSettings { get; }
        

        private readonly ReplaySubject<Response> responseSubject = new ReplaySubject<Response>(TimeSpan.FromMinutes(1));
        public IObservable<Response> ResponseObservable => responseSubject;


        private readonly ReplaySubject<Message> requestSubject = new ReplaySubject<Message>(TimeSpan.FromMinutes(1));
        public IObservable<Message> RequestObservable => requestSubject;


        private readonly ReplaySubject<Event> eventSubject = new ReplaySubject<Event>(TimeSpan.FromMinutes(1));
        public IObservable<Event> EventObservable => eventSubject;


        private readonly EventParser eventParser;

        static HeosClient()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new UnderscorePropertyNamesContractResolver()
            };
        }

        public HeosClient(IHeosTelnetClient heosTelnetClient, CancellationToken cancellationToken)
        {
            eventParser = new EventParser();
            this.cancellationToken = cancellationToken;
            this.heosTelnetClient = heosTelnetClient;

            heosTelnetClient.Messages.Subscribe(message =>
            {
                try
                {
                    ParseResponse(message.Trim());
                }
                catch (Exception exception)
                {
                    Logger.Error(exception.ToString());
                }
            });
        }

        private void ParseResponse(string responseString)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse>(responseString, JsonSerializerSettings);

            if (!string.IsNullOrWhiteSpace(heosResponse.Heos.Message) && heosResponse.Heos.Message.Contains("command under process"))
            {
                Logger.Warn("Ignoring command under process");
                return;
            }


            if (heosResponse.Heos.Command.StartsWith("event", StringComparison.OrdinalIgnoreCase))
            {
                var @event = eventParser.Create(heosResponse.Heos);
                if (@event != null)
                {
                    Logger.Info($"Event parsed: {@event.GetType().Name}");
                    eventSubject.OnNext(@event);
                }
            }
            else
            {
                var query = QueryHelpers.ParseQuery(heosResponse.Heos.Message);
                
                var sequenceString = query.ContainsKey(Constants.Sequence) ? query[Constants.Sequence] : null;

                var success = heosResponse.Heos.Result.Equals("success", StringComparison.OrdinalIgnoreCase);
                var sequence = -1;
                if (!string.IsNullOrWhiteSpace(sequenceString))
                {
                    sequence = int.Parse(sequenceString);
                    Logger.Info($"Sequence found: {sequence}");
                }
                else
                {
                    var commandAndMessage = heosResponse.Heos.Command;
                    if (!string.IsNullOrWhiteSpace(heosResponse.Heos.Message))
                    {
                        commandAndMessage += "?" + heosResponse.Heos.Message;
                    }

                    // HACK:
                    commandAndMessage = commandAndMessage.Replace("signed_in&", string.Empty);

                    Logger.Info($"Awaiting request match: {commandAndMessage}");
                    // TODO: StringComparison.InvariantCultureIgnoreCase
                    var command = RequestObservable
                        .Where(r => commandAndMessage.ToLowerInvariant().Contains(r.Text.ToLowerInvariant()) ||
                            r.Text.ToLowerInvariant().Contains(commandAndMessage.ToLowerInvariant()))
                        .TakeSynchronousNotifications().LastOrDefault();

                    // TODO: 
                    if (command != null)
                    {
                        sequence = command.Sequence;
                        Logger.Info($"Awaiting sequence found: {sequence}");
                    }
                    else
                    {
                        // TODO: log
                        Logger.Error($"Awaiting sequence not found: {commandAndMessage}");
                    }
                }

                var response = new Response(sequence, success, heosResponse, responseString);

                responseSubject.OnNext(response);
            }

          
        }

        public async Task Write(Message message)
        {
            var text = message.Generate();
            requestSubject.OnNext(message);
            await heosTelnetClient.WriteLine(text);
        }
    }
}
