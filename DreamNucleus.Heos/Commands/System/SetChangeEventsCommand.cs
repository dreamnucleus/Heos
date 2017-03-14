using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DreamNucleus.Heos.Infrastructure.Heos;


namespace DreamNucleus.Heos.Commands.System
{
    public sealed class SetChangeEventsCommand : Command<EmptyResponse>
    {
        public bool Enable { get; }

        public SetChangeEventsCommand(bool enable)
            : base($"system/register_for_change_events?enable={(enable ? "on" : "off")}")
        {
            Enable = enable;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }

}
