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
        public PlayStates PlayState { get; }

        public SetPlayStateCommand(int playerId, PlayStates playState)
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
        public PlayStates PlayState { get; } = PlayStates.Unknown;

        public PlayStateResponse()
        {
        }

        public PlayStateResponse(PlayStates playState)
        {
            PlayState = playState;
        }
    }

    public enum PlayStates
    {
        Unknown,
        Play,
        Pause,
        Stop
    }
}
