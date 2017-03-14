using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class BrowseServerCommand : Command<List<BrowseServerResponse>>
    {
        public int SourceId { get; }
        public BrowseServerCommand(int sourceId)
            : base ($"browse/browse?sid={sourceId}")
        {
        }

        public override List<BrowseServerResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<BrowseServerResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public sealed class BrowseServerResponse
    {
        public string Container { get; set; }
        public string Playable { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Cid { get; set; }
        public string Mid { get; set; }
    }

}
