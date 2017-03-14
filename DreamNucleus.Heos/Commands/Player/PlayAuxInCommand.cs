using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class PlayAuxInCommand : Command<EmptyResponse>
    {
        public int PlayerId { get; }
        public int AuxInId { get; }

        public PlayAuxInCommand(int playerId)
            : base($"browse/play_stream?pid={playerId}&sid={playerId}&mid=inputs/aux_in_1")
        {
            PlayerId = playerId;
            AuxInId = playerId;
        }

        public PlayAuxInCommand(int playerId, string mid)
            : base($"browse/play_stream?pid={playerId}&sid={playerId}&mid={mid}")
        {
            PlayerId = playerId;
            AuxInId = playerId;
        }

        public PlayAuxInCommand(int playerId, int auxInId)
            : base($"browse/play_stream?pid={playerId}&sid={auxInId}&mid=inputs/aux_in_1")
        {
            PlayerId = playerId;
            AuxInId = auxInId;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }

}
