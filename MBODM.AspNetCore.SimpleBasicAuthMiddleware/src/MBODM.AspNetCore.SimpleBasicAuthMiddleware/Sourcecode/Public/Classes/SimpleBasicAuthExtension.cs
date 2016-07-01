using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace MBODM.AspNetCore.SimpleBasicAuthMiddleware
{
    public static class SimpleBasicAuthExtension
    {
        public static IApplicationBuilder UseSimpleBasicAuth(this IApplicationBuilder applicationBuilder, string realm, Func<string, string, bool> authenticator)
        {
            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            return applicationBuilder.UseMiddleware<SimpleBasicAuth>(realm, authenticator);
        }
    }
}
