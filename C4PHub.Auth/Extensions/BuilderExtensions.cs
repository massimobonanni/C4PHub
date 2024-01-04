using C4PHub.Auth.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static  class BuilderExtensions
    {
        public static IApplicationBuilder UseEasyAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EasyAuthMiddleware>();
        }
    }
}
