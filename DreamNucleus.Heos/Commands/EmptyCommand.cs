using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands
{
    public sealed class EmptyCommand : Command<EmptyResponse>
    {
        public EmptyCommand()
            : base(string.Empty)
        {
        }

        public override EmptyResponse Parse(Response response)
        {
            throw new NotImplementedException();
        }
    }
}
