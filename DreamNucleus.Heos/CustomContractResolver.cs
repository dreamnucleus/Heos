using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DreamNucleus.Heos
{
    // https://c2experience.com/blog/2016/06/using-custom-contract-resolvers-for-jsonnet/
    public abstract class CustomContractResolver<TClass> : UnderscorePropertyNamesContractResolver
    {
        protected Dictionary<string, string> PropertyMappings { get; set; }

        protected void AddMap<TProperty>(Expression<Func<TClass, TProperty>> expression)
        {
            if (PropertyMappings == null)
            {
                PropertyMappings = new Dictionary<string, string>();
            }
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) return;
            PropertyMappings.Add(memberExpression.Member.Name, memberExpression.Member.Name);
        }

        protected void AddMap<TProperty>(Expression<Func<TClass, TProperty>> expression, string jsonPropertyName)
        {
            if (PropertyMappings == null)
            {
                PropertyMappings = new Dictionary<string, string>();
            }
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) return;
            PropertyMappings.Add(memberExpression.Member.Name, jsonPropertyName);
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName = null;
            var resolved = PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }
    }
}
