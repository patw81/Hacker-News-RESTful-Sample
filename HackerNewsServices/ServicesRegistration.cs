using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HackerNewsServices
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Here register all services
            services.TryAddTransient<IStoryService, StoryService>();

            return services;
        }
    }
}
