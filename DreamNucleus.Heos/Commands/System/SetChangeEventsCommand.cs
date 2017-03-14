using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.System
{
    public sealed class SetChangeEventsCommand : Command<ChangeEventsResponse>
    {
        public bool Enable { get; }

        public SetChangeEventsCommand(bool enable)
            : base($"system/register_for_change_events?enable={(enable ? "on" : "off")}")
        {
            Enable = enable;
        }

        public override ChangeEventsResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var state = query["enable"].Single();
            return new ChangeEventsResponse(state.Equals("on", StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class ChangeEventsResponse
    {
        public bool Enabled { get; set; }

        public ChangeEventsResponse(bool enabled)
        {
            Enabled = enabled;
        }

        public ChangeEventsResponse()
        {
        }
    }
}
