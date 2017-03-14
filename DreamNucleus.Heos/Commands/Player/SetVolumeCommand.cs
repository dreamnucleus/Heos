using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class SetVolumeCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }
        public int Volume { get; }

        public SetVolumeCommand(int playerId, int volume)
            : base($"player/set_volume?pid={playerId}&level={volume}")
        {
            PlayerId = playerId;
            Volume = volume;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
