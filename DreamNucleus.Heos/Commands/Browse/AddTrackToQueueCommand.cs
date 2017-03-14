using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class AddTrackToQueueCommand : Command<bool>
    {
        public AddTrackToQueueCommand(int playerId, int sourceId, string containerId, string musicId, int aid)
            : base($"browse/add_to_queue?pid={playerId}&sid={sourceId}&cid={containerId}&mid={musicId}&aid={aid}")
        {

        }
        public AddTrackToQueueCommand(int playerId, int sourceId, string containerId, int aid)
            : base($"browse/add_to_queue?pid={playerId}&sid={sourceId}&cid={containerId}&aid={aid}")
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
