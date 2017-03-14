using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public class ToggleMuteCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }

        public ToggleMuteCommand(int playerId)
            : base($"player/toggle_mute?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
