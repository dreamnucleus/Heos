using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public class SetGroupMute : Command<EmptyResponse>
    {
        public int GroupId { get; }
        public bool Mute { get; }

        public SetGroupMute(int groupId, bool mute)
            : base($"group/set_mute?gid={groupId}&state={(mute ? "on" : "off")}")
        {
            GroupId = groupId;
            Mute = mute;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
