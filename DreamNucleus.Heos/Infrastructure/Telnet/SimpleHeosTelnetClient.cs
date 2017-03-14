using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Interfaces;
using NLog;
using PrimS.Telnet;

namespace DreamNucleus.Heos.Infrastructure.Telnet
{
    public class SimpleHeosTelnetClient : IHeosTelnetClient, IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Client telnetClient;
        private readonly CancellationTokenSource cancellationTokenSource;

        private readonly Subject<string> messageSubject = new Subject<string>();
        public IObservable<string> Messages => messageSubject;


        public SimpleHeosTelnetClient(params string[] hosts)
        {
            cancellationTokenSource = new CancellationTokenSource();
            telnetClient = new Client(hosts.First(), 1255, cancellationTokenSource.Token);
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        var reply = await telnetClient.ReadAsync(TimeSpan.FromSeconds(20));
                        if (!string.IsNullOrWhiteSpace(reply))
                        {
                            var responses = reply.Split('\r', '\n');
                            foreach (var response in responses)
                            {
                                if (!string.IsNullOrWhiteSpace(response))
                                {
                                    Logger.Trace("Telnet read: " + response);
                                    messageSubject.OnNext(reply);
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.Error(exception, "Telnet read error.");
                    }
                }
            });
        }



        public async Task WriteLine(string command)
        {
            Logger.Trace("Telnet write: " + command);
            await telnetClient.WriteLine(command);
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            telnetClient.Dispose();
            messageSubject.Dispose();
        }
    }
}
