using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class SetVolumeCommand : Command<VolumeResponse>
    {
        public int PlayerId { get; }
        public int Volume { get; }

        public SetVolumeCommand(int playerId, int volume)
            : base($"player/set_volume?pid={playerId}&level={volume}")
        {
            PlayerId = playerId;
            Volume = volume;
        }

        public override VolumeResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var level = query["level"].Single();
            return new VolumeResponse(int.Parse(level));
        }
    }
}
