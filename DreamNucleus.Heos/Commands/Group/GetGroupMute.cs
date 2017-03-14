using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Group
{
    public class GetGroupMute : Command<GroupMuteResponse>
    {
        public int GroupId { get; }

        public GetGroupMute(int groupId)
            : base($"group/get_mute?gid={groupId}")
        {
            GroupId = groupId;
        }

        public override GroupMuteResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            return new GroupMuteResponse(int.Parse(query["gid"].Single()), query["state"].Single().Equals("on", StringComparison.OrdinalIgnoreCase));
        }
    }


    public sealed class GroupMuteResponse
    {
        public int GroupId { get; }
        public bool Mute { get; }

        public GroupMuteResponse(int groupId, bool mute)
        {
            GroupId = groupId;
            Mute = mute;
        }

        public GroupMuteResponse()
        {
        }
    }
}
