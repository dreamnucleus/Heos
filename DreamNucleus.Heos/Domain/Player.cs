using System;

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
}
