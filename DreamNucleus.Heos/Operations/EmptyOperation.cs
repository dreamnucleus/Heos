using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Operations
{
    public class EmptyOperation : Operation<EmptyPayload>
    {
        public override Task<EmptyPayload> Execute(CommandProcessor commandProcessor, CancellationToken cancellationToken)
        {
            return Task.FromResult(new EmptyPayload());
        }
    }
}
