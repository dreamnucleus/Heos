using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Events
{
    public class PlayerNowPlayingProgressEvent : Event
    {
        public int PlayerId { get; }
        public int Position { get; }

        public PlayerNowPlayingProgressEvent(Infrastructure.Heos.Heos response)
        {
            var query = QueryHelpers.ParseQuery(response.Message);
            var pid = query["pid"].Single();
            PlayerId = int.Parse(pid);
            Position = int.Parse(query["cur_pos"]);
        }
    }
}
