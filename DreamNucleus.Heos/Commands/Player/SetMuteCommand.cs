using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class SetMuteCommand : Command<MuteResponse>
    {
        public int PlayerId { get; }
        public bool Mute { get; }

        public SetMuteCommand(int playerId, bool mute)
            : base($"player/set_mute?pid={playerId}&state={(mute ? "on" : "off")}")
        {
            PlayerId = playerId;
            Mute = mute;
        }

        public override MuteResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            return new MuteResponse(query["state"].Single().Equals("on", StringComparison.OrdinalIgnoreCase));
        }
    }
}
