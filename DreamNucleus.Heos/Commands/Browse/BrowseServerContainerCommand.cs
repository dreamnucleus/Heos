using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class BrowseServerContainerCommand : Command<List<BrowseServerResponse>>
    {
        public int SourceId { get; set; }
        public string ContainerId { get; set; }

        public BrowseServerContainerCommand(int sourceId, string containerId)
            : base($"browse/browse?sid={sourceId}&cid={containerId}")
        {
            SourceId = sourceId;
            ContainerId = containerId;
        }

        public override List<BrowseServerResponse> Parse(Response response)
        {
            var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<BrowseServerResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
            return heosResponse.Payload;
        }
    }
}
