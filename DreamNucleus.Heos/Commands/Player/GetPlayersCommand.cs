using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Browse;
using DreamNucleus.Heos.Domain;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class GetPlayersCommand : Command<List<Domain.Player>, PlayerContractResolver>
    {
        public GetPlayersCommand()
            : base("player/get_players")
        {
        }

        //public override List<GetPlayerResponse> Parse(Response response)
        //{
        //    var heosResponse = JsonConvert.DeserializeObject<HeosResponse<List<GetPlayerResponse>>>(response.Message, HeosClient.JsonSerializerSettings);
        //    return heosResponse.Payload;
        //}
    }

    public sealed class GetPlayerResponse
    {
        public string Name { get; set; }
        public int Pid { get; set; }
        public int? Gid { get; set; }
        public string Version { get; set; }
        public string Model { get; set; }
        public string Ip { get; set; }
    }
}
