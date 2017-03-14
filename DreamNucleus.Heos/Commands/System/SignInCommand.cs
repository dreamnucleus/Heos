using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class SignInCommand : Command<EmptyResponse>
    {
        public SignInCommand(string username, string password)
            : base($"system/sign_in?un={username}&pw={password}")
        {

        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
