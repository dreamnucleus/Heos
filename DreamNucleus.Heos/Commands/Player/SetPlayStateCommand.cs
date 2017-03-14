using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands.Player
{
    public class SetPlayStateCommand : Command<PlayStateResponse>
    {
        public int PlayerId { get; }
        public PlayState PlayState { get; }

        public SetPlayStateCommand(int playerId, PlayState playState)
            : base($"player/set_play_state?pid={playerId}&state={playState.ToString().ToLower()} ")
        {
            PlayerId = playerId;
            PlayState = playState;
        }

        public override PlayStateResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var state = query["state"].Single();

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

    public class PlayStateResponse
    {
        public PlayState PlayState { get; } = PlayState.Unknown;

        public PlayStateResponse()
        {
        }

        public PlayStateResponse(PlayState playState)
        {
            PlayState = playState;
        }
    }

    public enum PlayState
    {
        Unknown,
        Play,
        Pause,
        Stop
    }
}
