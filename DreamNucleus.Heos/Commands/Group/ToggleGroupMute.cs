using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Group
{
    public class ToggleGroupMute : Command<EmptyResponse>
    {
        public int GroupId { get; }

        public ToggleGroupMute(int groupId)
            : base($"group/toggle_mute?gid={groupId}")
        {
            GroupId = groupId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
