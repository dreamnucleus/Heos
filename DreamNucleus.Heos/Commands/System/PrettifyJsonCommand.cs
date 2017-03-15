using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class PrettifyJsonCommand : Command<EmptyResponse>
    {
        public bool Enable { get; }

        public PrettifyJsonCommand(bool enable)
            : base($"system/prettify_json_response?enable{(enable ? "on" : "off")}")
        {
            Enable = enable;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
