using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public class GroupVolumeDownCommand : Command<EmptyResponse>
    {
        public int GroupId { get; }
        public int Step { get; }

        public GroupVolumeDownCommand(int groupId, int step = 5)
            : base($"group/volume_down?gid={groupId}&step={step}")
        {
            GroupId = groupId;
            Step = step;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
