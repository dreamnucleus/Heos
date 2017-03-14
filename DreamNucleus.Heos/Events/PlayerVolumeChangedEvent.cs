using System;
using System.Linq;
using DreamNucleus.Heos.Infrastructure.Helpers;

namespace DreamNucleus.Heos.Events
{
    public sealed class PlayerVolumeChangedEvent : Event
    {
        public int PlayerId { get; }
        public int Volume { get; }
        public bool Mute { get; set; }
        public PlayerVolumeChangedEvent(Infrastructure.Heos.Heos response)
        {
            var query = QueryHelpers.ParseQuery(response.Message);

            var pid = query["pid"];
            PlayerId = int.Parse(pid);

            var level = query["level"];
            Volume = int.Parse(level);

            var mute = query["mute"];
            Mute = mute.Equals("on", StringComparison.OrdinalIgnoreCase);
        }
    }
}
