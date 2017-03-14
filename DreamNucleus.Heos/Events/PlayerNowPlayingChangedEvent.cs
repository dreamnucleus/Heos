using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Events
{
    public class PlayerNowPlayingChangedEvent : Event
    {
        public int PlayerId { get; }

        public PlayerNowPlayingChangedEvent(Infrastructure.Heos.Heos response)
        {
            var query = QueryHelpers.ParseQuery(response.Message);
            var pid = query["pid"];
            PlayerId = int.Parse(pid);
        }
    }
}
