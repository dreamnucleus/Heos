using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Group
{
    public class GetGroupVolume : Command<GroupVolumeResponse>
    {
        public int GroupId { get; }

        public GetGroupVolume(int groupId)
            : base($"group/get_volume?gid={groupId}")
        {
            GroupId = groupId;
        }

        public override GroupVolumeResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);

            return new GroupVolumeResponse(int.Parse(query["gid"].Single()), int.Parse(query["level"].Single()));
        }
    }

    public class GroupVolumeResponse
    {
        public int GroupId { get; }
        public int Volume { get; }

        public GroupVolumeResponse(int groupId, int volume)
        {
            GroupId = groupId;
            Volume = volume;
        }

        public GroupVolumeResponse()
        {
        }
    }
}
