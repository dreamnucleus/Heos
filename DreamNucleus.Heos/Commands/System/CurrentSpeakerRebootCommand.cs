using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class CurrentSpeakerRebootCommand : Command<EmptyResponse>
    {
        public CurrentSpeakerRebootCommand()
            : base("system/reboot")
        {
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
