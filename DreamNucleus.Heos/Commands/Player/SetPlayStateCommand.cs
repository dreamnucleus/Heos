using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public class SetPlayStateCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }
        public PlayState PlayState { get; }

        public SetPlayStateCommand(int playerId, PlayState playState)
            : base($"player/set_play_state?pid={playerId}&state={playState.ToString().ToLower()} ")
        {
            PlayerId = playerId;
            PlayState = playState;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
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
