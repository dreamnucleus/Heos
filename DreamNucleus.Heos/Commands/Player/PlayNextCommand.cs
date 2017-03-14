using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class PlayNextCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }
        public PlayNextCommand(int playerId)
            : base($"player/play_next?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return new EmptyResponse();
        }
    }

}
