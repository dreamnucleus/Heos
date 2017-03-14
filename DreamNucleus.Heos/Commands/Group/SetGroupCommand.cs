using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public class SetGroupCommand : Command<EmptyResponse>
    {
        public int[] PlayerIds { get; }

        public SetGroupCommand(params int[] playerIds)
            : base($"group/set_group?pid={string.Join(",", playerIds)}")
        {
            PlayerIds = playerIds;
        }

        public override EmptyResponse Parse(Response response)
        {
            // TODO:
            return Empty;
        }
    }
}
