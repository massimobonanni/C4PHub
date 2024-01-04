using C4PHub.Auth.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace C4PHub.Auth.Middlewares
{
    internal class EasyAuthMiddleware
    {

        private readonly RequestDelegate _next;

        public EasyAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var principalId = context.Request.GetServicePrincipalId();
            if (!string.IsNullOrWhiteSpace(principalId))
            {
                var principalName = context.Request.GetServicePrincipalName();
                var identityProvider = context.Request.GetIdP();
                string jsonResult = await GetUserInfoFromIdP(context);

                // Create Identity from JSON
                var identity = ParseUserInfo(jsonResult, principalId, principalName, identityProvider);

                // Set current thread user to identity
                context.User = new GenericPrincipal(identity, null);
            };

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }

        /// <summary>
        /// Pasrse user info in JSON format to create the identity
        /// </summary>
        /// <param name="jsonResult"></param>
        /// <returns></returns>
        private ClaimsIdentity ParseUserInfo(string json, string principalId, string principalName, IdentityProvider idp)
        {
            var obj = JArray.Parse(json);

            var claims = new List<Claim>();


            string user_id = obj[0]["user_id"].Value<string>(); //user_id
            claims.Add(new Claim("user_id", user_id, null, "EasyAuthMiddleware"));

            foreach (var claim in obj[0]["user_claims"])
            {
                claims.Add(new Claim(claim["typ"].ToString(), claim["val"].ToString()));
            }

            var username = GetUsernameFromClaimsBasedOnIdP(claims, idp);

            var identity = new GenericIdentity(username ?? principalName);
            identity.Label = principalId;
            identity.AddClaims(claims);

            return identity;
        }

        /// <summary>
        /// Call /.auth/me to get user info.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<string> GetUserInfoFromIdP(HttpContext context)
        {
            var cookieContainer = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };
            string uriString = $"{context.Request.Scheme}://{context.Request.Host}";
            foreach (var c in context.Request.Cookies)
            {
                cookieContainer.Add(new Uri(uriString), new Cookie(c.Key, c.Value));
            }
            string jsonResult = string.Empty;
            using (HttpClient client = new HttpClient(handler))
            {
                var res = await client.GetAsync($"{uriString}/.auth/me");
                jsonResult = await res.Content.ReadAsStringAsync();
            }

            return jsonResult;
        }

        /// <summary>
        /// Get the username from claims based on IdentityProvider
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="idp"></param>
        /// <returns></returns>
        private string GetUsernameFromClaimsBasedOnIdP(IEnumerable<Claim> claims, IdentityProvider idp)
        {
            var username = claims
                .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                .FirstOrDefault()?
                .Value;

            return username;
        }
    }
}
