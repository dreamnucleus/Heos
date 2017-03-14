using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class GetVolumeCommand : Command<VolumeResponse>
    {
        public int PlayerId { get; }

        public GetVolumeCommand(int playerId)
            : base($"player/get_volume?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override VolumeResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var level = query["level"].Single();
            return new VolumeResponse(int.Parse(level));
        }
    }

    public sealed class VolumeResponse
    {
        public int Volume { get; }

        public VolumeResponse(int volume)
        {
            Volume = volume;
        }

        public VolumeResponse()
        {
        }
    }
}
