using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public class SetPlayModeCommand : Command<PlayModeResponse>
    {
        public int PlayerId { get; }
        public RepeatStates RepeatState { get; }
        public ShuffleStates ShuffleState { get; }

        public SetPlayModeCommand(int playerId, RepeatStates repeatState, ShuffleStates shuffleState)
            : base($"player/set_play_mode?pid={playerId}&repeat={repeatState.ToCommandString()}&shuffle={shuffleState.ToString().ToLower()}")
        {
            PlayerId = playerId;
            RepeatState = repeatState;
            ShuffleState = shuffleState;
        }

        public override PlayModeResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);
            var shuffle = query["shuffle"];

            // TODO: fix
            if (!Enum.TryParse(shuffle, true, out ShuffleStates shuffleState))
            {
                shuffleState = ShuffleStates.Unknown;
            }

            var repeat = query["repeat"];

            var repeatState = RepeatStates.Unknown;
            foreach (RepeatStates repeatStateToTest in Enum.GetValues(typeof(RepeatStates)))
            {
                if (repeatState != RepeatStates.Unknown && repeatState.ToCommandString().Equals(repeat, StringComparison.OrdinalIgnoreCase))
                {
                    repeatState = repeatStateToTest;
                }
            }

            return new PlayModeResponse(repeatState, shuffleState);
        }
    }

    public class PlayModeResponse
    {
        public RepeatStates RepeatState { get; } = RepeatStates.Unknown;
        public ShuffleStates ShuffleState { get; } = ShuffleStates.Unknown;

        public PlayModeResponse()
        {
        }

        public PlayModeResponse(RepeatStates repeatState, ShuffleStates shuffleState)
        {
            RepeatState = repeatState;
            ShuffleState = shuffleState;
        }
    }

    public enum RepeatStates
    {
        Unknown,
        OnAll,
        OnOne,
        Off
    }

    public static class RepeatStatesExtensions
    {
        public static string ToCommandString(this RepeatStates repeatState)
        {
            switch (repeatState)
            {
                case RepeatStates.OnAll:
                    return "on_all";
                case RepeatStates.OnOne:
                    return "on_one";
                case RepeatStates.Off:
                    return "off";
                default:
                    throw new ArgumentOutOfRangeException(nameof(repeatState), repeatState, null);
            }
        }
    }

    public enum ShuffleStates
    {
        Unknown,
        On,
        Off
    }
}

