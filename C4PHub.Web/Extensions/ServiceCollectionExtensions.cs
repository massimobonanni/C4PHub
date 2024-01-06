using C4PHub.Core.Implementations;
using C4PHub.Core.Interfaces;
using C4PHub.OpenAI.Implementations;
using C4PHub.Sessionize.Implementations;

namespace Microsoft.AspNetCore.Builder;

public static class ServiceCollectionExtensions
{
    public static void AddC4PManager(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddScoped<IUrlResolver, CoreUrlresolver>();
        services.AddKeyedScoped<IC4PExtractor, SessionizeC4PExtractor>("extractors");
        services.AddKeyedScoped<IC4PExtractor, OpenAIC4PExtractor>("extractors");
        services.AddScoped<IC4PExtractorFactory, CoreExtractorFactory>();
        services.AddScoped<IC4PManager, CoreC4PManager>();
        services.AddScoped<IFeedService, CoreFeedService>();
    }
}

