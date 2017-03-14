using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class GetMusicSourcesCommand : Command<List<GetMusicSourcesResponse>>
    {
        public GetMusicSourcesCommand()
            : base("browse/get_music_sources")
        {
        }

        public override List<GetMusicSourcesResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<GetMusicSourcesResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public sealed class GetMusicSourcesResponse
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public int Sid { get; set; }
    }
}
