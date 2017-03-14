using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public sealed class SetGroupVolume : Command<EmptyResponse>
    {
        public int GroupId { get; }
        public int Volume { get; }

        public SetGroupVolume(int groupId, int volume)
            : base($"group/set_volume?gid={groupId}&level={volume}")
        {
            GroupId = groupId;
            Volume = volume;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
