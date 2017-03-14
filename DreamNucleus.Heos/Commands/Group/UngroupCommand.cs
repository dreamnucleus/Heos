using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Group
{
    public class UngroupCommand : Command<EmptyResponse>
    {
        public int GroupId { get; }

        public UngroupCommand(int groupId)
            : base($"group/set_group?pid={groupId}")
        {
            GroupId = groupId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
