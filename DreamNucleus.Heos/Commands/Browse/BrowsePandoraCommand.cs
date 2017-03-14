using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class BrowsePandoraCommand : Command<List<BrowsePandoraResponse>>
    {
        public int SourceId { get; }
        public BrowsePandoraCommand(int sourceId, string mediaId)
            : base ($"browse/browse?sid={sourceId}&mid={mediaId}")
        {
        }

        public override List<BrowsePandoraResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<BrowsePandoraResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public sealed class BrowsePandoraResponse
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public int Mid { get; set; }
    }

}
