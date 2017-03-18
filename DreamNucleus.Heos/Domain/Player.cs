using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DreamNucleus.Heos.Commands.Player;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DreamNucleus.Heos.Domain
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? GroupId { get; set; }

        public string Version { get; set; }

        public Models Model { get; set; }

        public string IpAddress { get; set; }
    }

    public enum Models
    {
        Heos1,
        Heos3,
        Heos5,
        Heos7,
        // TODO: add in other devices
    }

    public class PlayerContractResolver : CustomContractResolver<Player>
    {
        public PlayerContractResolver()
        {
            AddMap(x => x.Id, "Pid");
            AddMap(x => x.GroupId, "Gid");
            AddMap(x => x.IpAddress, "Ip");
        }
    }

    public class ModelsJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                // TODO: should not be null?
                return null;
            }

            switch ((string)reader.Value)
            {
                case "HEOS 1":
                    return Models.Heos1;
                case "HEOS 3":
                    return Models.Heos3;
                case "HEOS 5":
                    return Models.Heos5;
                case "HEOS 7":
                    return Models.Heos7;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Models).IsAssignableFrom(objectType);
        }
    }

    public static class PlayerExtensions
    {
        public static GetNowPlayingCommand GetNowPlaying(this Player player)
        {
            return new GetNowPlayingCommand(player.Id);
        }

        // controls
        public static GetQueueCommand GetQueue(this Player player)
        {
            return new GetQueueCommand(player.Id);
        }

        public static GetPlayStateCommand GetPlayState(this Player player)
        {
            return new GetPlayStateCommand(player.Id);
        }
        public static SetPlayStateCommand SetPlayState(this Player player, PlayStates playState)
        {
            return new SetPlayStateCommand(player.Id, playState);
        }

        // TODO:GetPlayMode

        public static SetPlayModeCommand SetPlayMode(this Player player, RepeatStates repeatState, ShuffleStates shuffleState)
        {
            return new SetPlayModeCommand(player.Id, repeatState, shuffleState);
        }

        public static PlayNextCommand PlayNext(this Player player)
        {
            return new PlayNextCommand(player.Id);
        }

        public static PlayPreviousCommand PlayPrevious(this Player player)
        {
            return new PlayPreviousCommand(player.Id);
        }



        // volume
        public static GetVolumeCommand GetVolume(this Player player)
        {
            return new GetVolumeCommand(player.Id);
        }

        public static SetVolumeCommand SetVolume(this Player player, int volume)
        {
            return new SetVolumeCommand(player.Id, volume);
        }

        public static VolumeUpCommand VolumeUp(this Player player)
        {
            return new VolumeUpCommand(player.Id);
        }

        public static VolumeDownCommand VolumeDown(this Player player)
        {
            return new VolumeDownCommand(player.Id);
        }


        // mute
        public static GetMuteCommand GetMute(this Player player)
        {
            return new GetMuteCommand(player.Id);
        }

        public static SetMuteCommand SetMute(this Player player, bool mute)
        {
            return new SetMuteCommand(player.Id, mute);
        }

        public static ToggleMuteCommand ToggleMute(this Player player)
        {
            return new ToggleMuteCommand(player.Id);
        }
    }
}
