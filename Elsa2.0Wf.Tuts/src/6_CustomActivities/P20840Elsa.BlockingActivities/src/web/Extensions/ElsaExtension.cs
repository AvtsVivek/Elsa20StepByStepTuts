using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Server.Web.Extensions
{
    public static class ElsaExtension
    {
        // This is a temp addition till elsa next version.
        // The latest version contins this. But the current version does not.
        // https://github.com/elsa-workflows/elsa-core/blob/master/src/core/Elsa.Core/Extensions/MessageHandlerServiceCollectionExtensions.cs
        public static IServiceCollection AddNotificationHandlersFrom<TMarker>(this IServiceCollection services)
        { 
            services.AddNotificationHandlers(typeof(TMarker));
            return services;
        }
    }
}
