using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public class ModifyGroupCommand : Command<EmptyResponse>
    {
        public int GroupId { get; }
        public int[] PlayerIds { get; }

        public ModifyGroupCommand(int groupId, params int[] playerIds)
            : base($"group/set_group?pid={groupId + (playerIds.Any() ? "," + string.Join(",", playerIds) : string.Empty)}")
        {
            GroupId = groupId;
            PlayerIds = playerIds;
        }

        public override EmptyResponse Parse(Response response)
        {
            // TODO:
            return Empty;
        }
    }
}
