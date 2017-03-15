using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DreamNucleus.Heos.Commands;
using DreamNucleus.Heos.Infrastructure.Extensions;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos
{
    public class CommandProcessor
    {
        private static readonly ILog Logger = LogManager.GetLogger<CommandProcessor>();

        private readonly HeosClient heosClient;

        public int Retry { get; set; } = 0;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(1); // TODO: choose less
        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromMilliseconds(300);


        public CommandProcessor(HeosClient heosClient)
        {
            this.heosClient = heosClient;
        }

        public async Task Start(Message message)
        {
            await heosClient.Write(message);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command) where T : new()
        {
            return await Execute(command, p => true, Retry, Timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, int retry) where T : new()
        {
            return await Execute(command, p => true, retry, Timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, TimeSpan timeout) where T : new()
        {
            return await Execute(command, p => true, Retry, timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, int retry, TimeSpan timeout) where T : new()
        {
            return await Execute(command, p => true, retry, timeout);
        }
        public async Task<Response<T>> Execute<T>(Command<T> command, int retry, TimeSpan timeout, TimeSpan retryDelay) where T : new()
        {
            return await Execute(command, p => true, retry, timeout, retryDelay);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, Func<T, bool> successFunc) where T : new()
        {
            return await Execute(command, successFunc, Retry, Timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, Func<T, bool> successFunc, int retry)
            where T : new()
        {
            return await Execute(command, successFunc, retry, Timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, Func<T, bool> successFunc, TimeSpan timeout)
            where T : new()
        {
            return await Execute(command, successFunc, Retry, timeout);
        }

        public async Task<Response<T>> Execute<T>(Command<T> command, Func<T, bool> successFunc, int retry,
            TimeSpan timeout) where T : new()
        {
            return await Execute(command, successFunc, retry, timeout, RetryDelay);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command">The command to execute</param>
        /// <param name="successFunc">How to determine if the result is what you wanted</param>
        /// <param name="retry">How many reties, 0 is no reties</param>
        /// <param name="timeout">How long to wait for the response</param>
        /// <param name="retryDelay">How long to wait before the retry</param>
        /// <returns></returns>
        public async Task<Response<T>> Execute<T>(Command<T> command, Func<T, bool> successFunc, int retry,
            TimeSpan timeout, TimeSpan retryDelay) where T : new()
        {
            return await Observable.FromAsync(async () =>
            {
                await Start(command);
                return await heosClient.ResponseObservable.FirstAsync(r => r.Sequence == command.Sequence)
                    .Timeout(timeout)
                    .Select(r =>
                    {
                        if (r.Success)
                        {
                            return new Response<T>(r, command.Parse(r));
                        }
                        else
                        {
                            Logger.Error($"Failed request: {command.Text}");
                            return new Response<T>(new Response(command.Sequence), command.Empty);
                        }
                    });
            })
            .FirstAsync(r => successFunc(r.Payload))
            .RetryAfterDelay<Response<T>, Exception>(retryDelay, retry)
            .Catch(Observable.Return(new Response<T>(new Response(command.Sequence), command.Empty)));
        }

    }
}
