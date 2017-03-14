using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.Browse
{
    public sealed class HeosLoginCommand : Command<bool>
    {
        public int SourceId { get; }
        public HeosLoginCommand(string user, string pw)
            : base ($"system/sign_in?un={user}&pw={pw}")
        {
        }

        public override bool Parse(Response response)
        {
            if (response.Received)
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
