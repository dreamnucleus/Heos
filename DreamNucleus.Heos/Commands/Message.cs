using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DreamNucleus.Heos.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;

namespace DreamNucleus.Heos.Commands
{
    public class Message
    {
        public int Sequence { get; private set; }
        public string Text { get; }


        protected Message(string text)
        {
            Text = text;
        }

        public string Generate()
        {
            // TODO: only gererate this once, then have a new create which changes sequence
            Sequence = Infrastructure.Sequences.Next();
            // HACK:
            return AddParameter("heos://" + Text.Replace("+", "%2B"), Constants.Sequence, Sequence.ToString());
        }

        private static string AddParameter(string text, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(text);

            var query = QueryHelpers.ParseQuery(uriBuilder.Query);

            query.Remove(paramName);
            query.Add(paramName, paramValue);

            uriBuilder.Query = string.Empty;

            return WebUtility.UrlDecode(QueryHelpers.AddQueryString(uriBuilder.ToString(), query.ToDictionary(q => q.Key, q => q.Value.Single())));
        }
    }
}
