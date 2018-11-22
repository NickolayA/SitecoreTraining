using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using MySite.ContentSearch.Repositories;
using Sitecore.DependencyInjection;

namespace MySite.Infrastructure
{
    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllersInCurrentAssembly();
            serviceCollection.AddSingleton<IPostsRepository, VenuesRepository>();
            // for glass mapper
            serviceCollection.AddSingleton<ISitecoreContext, SitecoreContext>();
        }
    }
}