﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using DreamNucleus.Heos.Infrastructure.Heos;
using DreamNucleus.Heos.Interfaces;
using PrimS.Telnet;

namespace DreamNucleus.Heos.Infrastructure.Telnet
{
    public class SimpleTelnetClient : IHeosTelnetClient, IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger<SimpleTelnetClient>();

        private readonly Client telnetClient;
        private readonly CancellationTokenSource cancellationTokenSource;

        private readonly Subject<string> messageSubject = new Subject<string>();
        public IObservable<string> Messages => messageSubject;


        public SimpleTelnetClient(params string[] hosts)
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
                            var responses = reply.Split(new [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var response in responses)
                            {
                                if (!string.IsNullOrWhiteSpace(response))
                                {
                                    Logger.Trace("Telnet read: " + response);
                                    messageSubject.OnNext(response);
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Logger.Error("Telnet read error.", exception);
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
