using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Browse;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class GetQueueCommand : Command<List<GetQueueResponse>>
    {
        public GetQueueCommand(int playerId)
            : base($"player/get_queue?pid={playerId}")
        {
        }

        public override List<GetQueueResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<GetQueueResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public class GetQueueResponse
    {
        public string Song { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
        public string Qid { get; set; }
        public string Mid { get; set; }
        public string AlbumId { get; set; }
    }
}
