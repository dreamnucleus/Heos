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
        public RepeatState RepeatState { get; }
        public ShuffleState ShuffleState { get; }

        public SetPlayModeCommand(int playerId, RepeatState repeatState, ShuffleState shuffleState)
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
            if (!Enum.TryParse(shuffle, true, out ShuffleState shuffleState))
            {
                shuffleState = ShuffleState.Unknown;
            }

            var repeat = query["repeat"];

            var repeatState = RepeatState.Unknown;
            foreach (RepeatState repeatStateToTest in Enum.GetValues(typeof(RepeatState)))
            {
                if (repeatState != RepeatState.Unknown && repeatState.ToCommandString().Equals(repeat, StringComparison.OrdinalIgnoreCase))
                {
                    repeatState = repeatStateToTest;
                }
            }

            return new PlayModeResponse(repeatState, shuffleState);
        }
    }

    public class PlayModeResponse
    {
        public RepeatState RepeatState { get; } = RepeatState.Unknown;
        public ShuffleState ShuffleState { get; } = ShuffleState.Unknown;

        public PlayModeResponse()
        {
        }

        public PlayModeResponse(RepeatState repeatState, ShuffleState shuffleState)
        {
            RepeatState = repeatState;
            ShuffleState = shuffleState;
        }
    }

    public enum RepeatState
    {
        Unknown,
        OnAll,
        OnOne,
        Off
    }

    public static class RepeatStatesExtensions
    {
        public static string ToCommandString(this RepeatState repeatState)
        {
            switch (repeatState)
            {
                case RepeatState.OnAll:
                    return "on_all";
                case RepeatState.OnOne:
                    return "on_one";
                case RepeatState.Off:
                    return "off";
                default:
                    throw new ArgumentOutOfRangeException(nameof(repeatState), repeatState, null);
            }
        }
    }

    public enum ShuffleState
    {
        Unknown,
        On,
        Off
    }
}

