using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure.Helpers;
using DreamNucleus.Heos.Infrastructure.Heos;

namespace DreamNucleus.Heos.Commands.System
{
    public sealed class AccountCheckCommand : Command<AccountCheckResponse>
    {
        public AccountCheckCommand()
            : base("system/check_account")
        {
        }

        public override AccountCheckResponse Parse(Response response)
        {
            var query = QueryHelpers.ParseQuery(response.HeosResponse.Heos.Message);

            var signedIn = query.Keys.Any(k => k.Equals("signed_in", StringComparison.OrdinalIgnoreCase));

            if (signedIn)
            {
                return new AccountCheckResponse(query["un"]);
            }
            else
            {
                return new AccountCheckResponse();
            }
        }
    }

    public class AccountCheckResponse
    {
        public bool SignedIn { get; }
        public string Username { get; }

        public AccountCheckResponse(string username)
        {
            SignedIn = true;
            Username = username;
        }

        public AccountCheckResponse()
        {
            SignedIn = false;
        }
    }
}
