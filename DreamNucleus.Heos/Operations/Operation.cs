using System;
using System.Threading;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Operations
{
    public abstract class Operation<T> : IOperation
        where T : OperationPayload
    {
        public abstract Task<T> Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken);

        public async Task<T> Execute(CommandProcessor commandProcessor)
        {
            return await Execute(commandProcessor, CancellationToken.None);
        }

        public async Task<OperationResult<T>> TryExecute(CommandProcessor commandProcessor)
        {
            return await TryExecute(commandProcessor, CancellationToken.None);
        }

        public async Task<OperationResult<T>> TryExecute(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            try
            {
                return new OperationResult<T>
                {
                    Success = true,
                    Payload = await Execute(commandProcessor, cancellationToken)
                };
            }
            catch (Exception)
            {
                return new OperationResult<T>
                {
                    Success = false
                };
            }
        }

        protected async Task Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken, params Func<Task>[] funcs)
        {
            foreach (var func in funcs)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await func();
            }
        }

        public async Task TryRun(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            await TryExecute(commandProcessor, cancellationToken);
        }
    }

    public interface IOperation
    {
        Task TryRun(CommandProcessor commandProcessor, CancellationToken cancellationToken);
    }


    public class OperationResult<T>
        where T : OperationPayload
    {
        public bool Success { get; set; }
        public T Payload { get; set; }
    }
    public abstract class OperationPayload
    {
    }
    public class EmptyPayload : OperationPayload
    {

    }
}
