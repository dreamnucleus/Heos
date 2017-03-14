using System.Linq;
using DreamNucleus.Heos.Infrastructure.Helpers;

namespace DreamNucleus.Heos.Events
{
    public class PlayerNowPlayingProgressEvent : Event
    {
        public int PlayerId { get; }
        public int Position { get; }

        public PlayerNowPlayingProgressEvent(Infrastructure.Heos.Heos response)
        {
            var query = QueryHelpers.ParseQuery(response.Message);

            PlayerId = int.Parse(query["pid"]);
            Position = int.Parse(query["cur_pos"]);
        }
    }
}
