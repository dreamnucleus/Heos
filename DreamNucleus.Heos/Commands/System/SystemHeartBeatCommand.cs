using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class SystemHeartBeatCommand : Command<EmptyResponse>
    {
        public SystemHeartBeatCommand()
            : base("system/heart_beat")
        {
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
