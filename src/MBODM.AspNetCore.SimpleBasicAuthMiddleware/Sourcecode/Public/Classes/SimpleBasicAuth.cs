using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MBODM.AspNetCore.SimpleBasicAuthMiddleware
{
    public sealed class SimpleBasicAuth
    {
        private readonly RequestDelegate next;
        private readonly string realm;
        private readonly Func<string, string, bool> authenticator;

        public SimpleBasicAuth(RequestDelegate next, string realm, Func<string, string, bool> authenticator)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (realm == null)
            {
                throw new ArgumentNullException(nameof(realm));
            }

            if (authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            this.next = next;
            this.realm = realm;
            this.authenticator = authenticator;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Authenticate(GetCredentials(context.Request));

                await next.Invoke(context);
            }
            catch (SimpleBasicAuthException)
            {
                SetResponse(context.Response);
            }
        }

        private KeyValuePair<string, string> GetCredentials(HttpRequest request)
        {
            var headers = request.Headers["Authorization"];

            if (!headers.Any())
            {
                throw new SimpleBasicAuthException("No authorization header found.");
            }

            var prefix = "Basic ";

            var header = headers.Where(h => h.ToLower().StartsWith(prefix.ToLower())).FirstOrDefault();

            if (header == null)
            {
                throw new SimpleBasicAuthException("No basic authentication header found.");
            }

            var encoded = header.Substring(prefix.Length);

            var decoded = string.Empty;

            try
            {
                decoded = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(encoded));
            }
            catch
            {
                throw new SimpleBasicAuthException("Invalid basic authentication header encoding.");
            }

            if (!decoded.Contains(":"))
            {
                throw new SimpleBasicAuthException("Invalid basic authentication header format.");
            }

            var credentials = decoded.Split(':');

            return new KeyValuePair<string, string>(credentials.First(), credentials.Last());
        }

        private void Authenticate(KeyValuePair<string, string> credentials)
        {
            if (!authenticator(credentials.Key, credentials.Value))
            {
                throw new SimpleBasicAuthException("Authentication failed.");
            }
        }

        private void SetResponse(HttpResponse response)
        {
            response.Headers.Add("WWW-Authenticate", $"Basic realm=\"{realm}\"");
            response.StatusCode = 401;
        }
    }
}
