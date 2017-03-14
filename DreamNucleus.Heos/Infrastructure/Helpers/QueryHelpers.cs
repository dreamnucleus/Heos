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
#if NET451
            return System.Web.HttpUtility.ParseQueryString(queryString).ToDictionary();
#else
            return Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(queryString).ToDictionary(q => q.Key, q => q.Value.Single());
#endif
        }

        public static string AddQueryString(string uri, IDictionary<string, string> queryString)
        {
#if NET451
            return uri + "?" + queryString.ToNameValueCollection().ToString();
#else
            return Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(uri, queryString);
#endif
        }

#if NET451
        private static IDictionary<string, string> ToDictionary(this System.Collections.Specialized.NameValueCollection nameValueCollection)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var name in nameValueCollection.AllKeys)
            {
                dictionary.Add(name, nameValueCollection[name]);
            }
            return dictionary;
        }

        public static System.Collections.Specialized.NameValueCollection ToNameValueCollection(this IDictionary<string, string> dictionary)
        {
            var nameValueCollection = new System.Collections.Specialized.NameValueCollection();

            foreach(var item in dictionary)
            {
                nameValueCollection.Add(item.Key, item.Value);
            }

            return nameValueCollection;
        }
#endif
    }
}
