using System;
using System.Collections.Generic;
using System.Linq;

namespace DreamNucleus.Heos.Domain
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? GroupId { get; set; }

        public string Version { get; set; }
        public string Model { get; set; }

        public string IpAddress { get; set; }
    }

    public class PlayerContractResolver : CustomContractResolver<Player>
    {
        public PlayerContractResolver()
        {
            AddMap(x => x.Id, "Pid");
            AddMap(x => x.GroupId, "Gid");
            AddMap(x => x.IpAddress, "Ip");
        }
    }
}
