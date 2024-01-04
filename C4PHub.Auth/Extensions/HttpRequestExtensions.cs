using C4PHub.Auth;
using C4PHub.Auth.Entities;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Mvc
{
    public static class HttpRequestExtensions
    {

        public static IDictionary<string, string> GetHeaders(this HttpRequest request)
        {
            var dict = new Dictionary<string, string>();
            foreach (var header in request.Headers)
            {
                dict.Add(header.Key, header.Value);
            }
            return dict;
        }

        public static IDictionary<string,string> GetCookies(this HttpRequest request)
        {
            var dict = new Dictionary<string, string>();
            foreach (var header in request.Cookies)
            {
                dict.Add(header.Key, header.Value);
            }
            return dict;
        }

        public static IdentityProvider GetIdP(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(Constants.EasyAuthPrincipalIdPHeader))
            {
                var idp = request.Headers[Constants.EasyAuthPrincipalIdPHeader];
                return ToIdentityProvider(idp);
            }
            return IdentityProvider.Unknown;
            
        }

        public static string? GetServicePrincipalId(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(Constants.EasyAuthPrincipalIdHeader))
                return request.Headers[Constants.EasyAuthPrincipalIdHeader];
            return null;
        }

        public static string? GetServicePrincipalName(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(Constants.EasyAuthPrincipalNameHeader))
                return request.Headers[Constants.EasyAuthPrincipalNameHeader];
            return null;
        }

        public static string? GetServicePrincipalToken(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(Constants.EasyAuthPrincipalTokenHeader))
                return request.Headers[Constants.EasyAuthPrincipalTokenHeader];
            return null;
        }

        public static string? GetAADPrincipalName(this HttpRequest request)
        {
            if (request.Headers.ContainsKey(Constants.EasyAuthAADPrincipalNameHeader))
                return request.Headers[Constants.EasyAuthAADPrincipalNameHeader];
            return null;
        }


        private static IdentityProvider ToIdentityProvider(string source)
        {
            switch (source.ToLower())
            {
                case "aad":
                    return IdentityProvider.MicrosoftEntraID;
                case "github":
                    return IdentityProvider.GitHub;
                default:
                    return IdentityProvider.Unknown;
            }
        }
    }
}
