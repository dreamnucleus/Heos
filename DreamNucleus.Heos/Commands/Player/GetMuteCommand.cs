using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class GetMuteCommand : Command<MuteResponse>
    {
        public int PlayerId { get; }

        public GetMuteCommand(int playerId)
            : base($"player/get_mute?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override MuteResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            return new MuteResponse(query["state"].Single().Equals("on", StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class MuteResponse
    {
        public bool Mute { get; set; }

        public MuteResponse(bool mute)
        {
            Mute = mute;
        }

        public MuteResponse()
        {
        }
    }
}
