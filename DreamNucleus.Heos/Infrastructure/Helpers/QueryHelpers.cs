using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Infrastructure.Helpers
{
    public static class QueryHelpers
    {
        public static IDictionary<string, string> ParseQuery(string queryString)
        {
#if NET45
            return System.Web.HttpUtility.ParseQueryString(queryString).ToDictionary();
#else
            return Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString).ToDictionary(q => q.Key, q => q.Value.Single());
#endif
        }

        public static string AddQueryString(string uri, IDictionary<string, string> queryString)
        {
#if NET45
            return uri + (queryString.Any() ? "?" + queryString.ToQueryString() : string.Empty);
#else
            return Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(uri, queryString);
#endif
        }

#if NET45
        private static IDictionary<string, string> ToDictionary(this System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var name in nameValueCollection.AllKeys)
            {
                dictionary.Add(name, nameValueCollection[name]);
            }
            return dictionary;
        }

        public static string ToQueryString(this IDictionary<string, string> dictionary)
        {
            return string.Join("&", dictionary.Select(d => d.Key + "=" + d.Value));
        }
#endif
    }
}
