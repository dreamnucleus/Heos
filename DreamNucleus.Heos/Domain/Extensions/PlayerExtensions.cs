using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Player;

namespace DreamNucleus.Heos.Domain.Extensions
{
    public static class PlayerExtensions
    {
        public static GetNowPlayingCommand GetNowPlaying(this Player player)
        {
            return new GetNowPlayingCommand(player.Id);
        }

        public static SetMuteCommand SetMute(this Player player, bool mute)
        {
            return new SetMuteCommand(player.Id, mute);
        }
    }
}
