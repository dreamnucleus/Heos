using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class BrowseSourceCommand : Command<List<BrowseSourceResponse>>
    {
        public int SourceId { get; }
        public BrowseSourceCommand(int sourceId)
            : base ($"browse/browse?sid={sourceId}")
        {
        }

        public override List<BrowseSourceResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<BrowseSourceResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }

    public sealed class BrowseSourceResponse
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
        public int Sid { get; set; }
    }

}
