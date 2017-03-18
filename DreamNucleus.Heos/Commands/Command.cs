using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Commands.Player;
using DreamNucleus.Heos.Infrastructure.Heos;
using Newtonsoft.Json;

namespace DreamNucleus.Heos.Commands
{
    public abstract class Command<TReturn> : Message
        where TReturn : new()
    {
        public abstract TReturn Parse(Response response);
        public TReturn Empty => new TReturn();

        protected Command(string text)
            : base(text)
        {
        }
    }

    public abstract class Command<TReturn, TCustomContractResolver> : Command<TReturn>
        where TReturn : new()
        where TCustomContractResolver : CustomContractResolver<UnderscorePropertyNamesContractResolver>, new()
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings;

        static Command()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new TCustomContractResolver()
            };
        }

        protected Command(string text)
            : base(text)
        {
        }

        public override TReturn Parse(Response response)
        {
            return JsonConvert.DeserializeObject<HeosResponse<TReturn>>(response.Message, JsonSerializerSettings).Payload;
        }
    }

    public abstract class Command : Command<EmptyResponse>
    {
        public override EmptyResponse Parse(Response response)
        {
            return Empty;
        }

        protected Command(string text)
            : base(text)
        {
        }
    }
}
