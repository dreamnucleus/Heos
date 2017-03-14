using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class VolumeUpCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }

        public VolumeUpCommand(int playerId)
            : base($"player/volume_up?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return new EmptyResponse();
        }
    }
}
