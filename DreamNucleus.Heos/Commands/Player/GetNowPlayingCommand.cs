using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class GetNowPlayingCommand : Command<NowPlayingResponse>
    {
        public GetNowPlayingCommand(int playerId)
            : base($"player/get_now_playing_media?pid={playerId}")
        {
        }

        public override NowPlayingResponse Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<NowPlayingResponse>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public sealed class NowPlayingResponse
    {
        public string Type { get; set; }
        public string Song { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
        public string Mid { get; set; }
        public string Qid { get; set; }
        public int? Sid { get; set; }
        public string AlbumId { get; set; }
    }
}
