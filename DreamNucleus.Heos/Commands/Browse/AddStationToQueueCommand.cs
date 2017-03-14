using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class AddStationToQueueCommand : Command<bool>
    {
        public AddStationToQueueCommand(int playerId, int sourceId, string containerId, string musicId, string stationName, int aid)
            : base($"browse/play_stream?pid={playerId}&sid={sourceId}&cid={containerId}&mid={musicId}&aid={aid}&name={stationName}")
        {

        }

        public override bool Parse(Response response)
        {
            if(response.Received)
            {
                return response.Success;
            }
            else
            {
                return false;
            }
        }
    }
}
