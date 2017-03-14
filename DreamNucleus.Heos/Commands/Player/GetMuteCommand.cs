using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Infrastructure.Heos;

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
            return new MuteResponse(query["state"].Equals("on", StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class MuteResponse
    {
        public bool Mute { get; }

        public MuteResponse(bool mute)
        {
            Mute = mute;
        }

        public MuteResponse()
        {
        }
    }
}
