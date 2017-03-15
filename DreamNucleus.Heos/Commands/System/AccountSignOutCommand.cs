using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public class AccountSignOutCommand : Command<EmptyResponse>
    {
        public AccountSignOutCommand()
            : base("/system/sign_out")
        {
        }

        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }
    }
}
