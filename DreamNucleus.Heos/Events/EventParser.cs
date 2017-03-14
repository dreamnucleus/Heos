using System;
using System.Collections.Generic;
using System.Linq;

namespace DreamNucleus.Heos.Events
{
    public sealed class EventParser
    {
        private readonly Dictionary<string, Func<Infrastructure.Heos.Heos, Event>> eventCreators = new Dictionary<string, Func<Infrastructure.Heos.Heos, Event>>();

        public EventParser()
        {
            eventCreators.Add("player_now_playing_changed", h => new PlayerNowPlayingChangedEvent(h));
            eventCreators.Add("player_volume_changed", h => new PlayerVolumeChangedEvent(h));
            eventCreators.Add("player_state_changed", h => new PlayerStateChangedEvent(h));
            eventCreators.Add("groups_changed", h => new GroupsChangedEvent());
            eventCreators.Add("player_now_playing_progress", h => new PlayerNowPlayingProgressEvent(h));
            eventCreators.Add("sources_changed", h => new SourcesChangedEvent(h));
        }

        public Event Create(Infrastructure.Heos.Heos heos)
        {
            var creator = eventCreators.SingleOrDefault(e => heos.Command.EndsWith(e.Key, StringComparison.OrdinalIgnoreCase)).Value;
            return creator?.Invoke(heos);
        }
    }
}
