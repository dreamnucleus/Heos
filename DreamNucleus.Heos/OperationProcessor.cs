using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DreamNucleus.Heos.Operations;
using NLog;
using NLog.Fluent;

namespace DreamNucleus.Heos
{
    public class OperationProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly CommandProcessor commandProcessor;

        private readonly BlockingCollection<IOperation> operations;

        public OperationProcessor(CommandProcessor commandProcessor)
        {
            this.commandProcessor = commandProcessor;
            this.operations = new BlockingCollection<IOperation>(new ConcurrentQueue<IOperation>());
            Task.Run(async () =>
            {
                Task task = null;
                var operationCancellationTokenSource = new CancellationTokenSource();
                while (true)
                {
                    try
                    {
                        Logger.Trace("Awaiting for operation from queue");
                        var operation = operations.Take();
                        Logger.Trace("Found operation");
                        operationCancellationTokenSource.Cancel();
                        if (task != null)
                        {
                            Logger.Trace("Awaiting for previous operation to exit");
                            await task;
                        }
                        operationCancellationTokenSource.Dispose();
                        operationCancellationTokenSource = new CancellationTokenSource();
                        Logger.Trace("Executing next operation");
                        task = operation.TryRun(commandProcessor, operationCancellationTokenSource.Token);
                    }
                    catch (Exception exception)
                    {
                        Logger.Error(exception, "Read loop exception:");
                    }
                }
            });
        }

        public void Execute(IOperation operation)
        {
            var added = operations.TryAdd(operation);
            if (!added)
            {
                Logger.Error("Could not add to operation queue.");
            }
        }
    }
}
