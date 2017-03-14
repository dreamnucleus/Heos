using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public class GetPlayStateCommand : Command<PlayStateResponse>
    {
        public int PlayerId { get; }

        public GetPlayStateCommand(int playerId)
            : base($"player/get_play_state?pid={playerId}")
        {
            PlayerId = playerId;
        }

        public override PlayStateResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var state = query["state"];

            if (Enum.TryParse(state, true, out PlayState playState))
            {
                return new PlayStateResponse(playState);
            }
            else
            {
                return new PlayStateResponse();
            }
        }
    }
}
