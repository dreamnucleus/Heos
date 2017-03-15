using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class AccountSignInCommand : Command<EmptyResponse>
    {
        public string Username { get; }
        public string Password { get; }

        public AccountSignInCommand(string username, string password)
            : base($"system/sign_in?un={username}&pw={password}")
        {
            Username = username;
            Password = password;
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
