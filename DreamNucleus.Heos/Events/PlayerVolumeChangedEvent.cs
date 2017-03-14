using System;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

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

            var pid = query["pid"].Single();
            PlayerId = int.Parse(pid);

            var level = query["level"].Single();
            Volume = int.Parse(level);

            var mute = query["mute"].Single();
            Mute = mute.Equals("on", StringComparison.OrdinalIgnoreCase);
        }
    }
}
