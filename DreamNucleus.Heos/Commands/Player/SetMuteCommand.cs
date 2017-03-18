using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Player
{
    public sealed class SetMuteCommand : Command
    {
        public int PlayerId { get; }
        public bool Mute { get; }

        public SetMuteCommand(int playerId, bool mute)
            : base($"player/set_mute?pid={playerId}&state={(mute ? "on" : "off")}")
        {
            PlayerId = playerId;
            Mute = mute;
        }
    }
}
