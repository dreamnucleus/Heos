using System;
using System.Collections.Generic;
using System.Text;
using DreamNucleus.Heos.Commands.Player;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Group
{
    public sealed class GetGroupInfoCommand : Command<GroupInfo>
    {
        public int GroupId { get; }

        public GetGroupInfoCommand(int groupId)
            : base($"group/get_group_info?gid={groupId}")
        {
            GroupId = groupId;
        }

        public override GroupInfo Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<GroupInfo>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public class GroupInfo
    {
        public string Name { get; set; }
        public int Gid { get; set; }

        public ICollection<GroupPlayerInfo> Players { get; set; }
    }

    public class GroupPlayerInfo
    {
        public string Name { get; set; }
        public int Pid { get; set; }
        public GroupRoles Role { get; set; }
    }

}
