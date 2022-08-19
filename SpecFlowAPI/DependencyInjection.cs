using BoDi;
using Bogus;

namespace SpecFlowAPI
{
    class DependencyInjection
    {
        public static void ConfigureDependencies(IObjectContainer container, GlobalSettings globalSettings)
        {
            RegisterConfiguration(container, globalSettings);
            RegisterServices(container, globalSettings);
            RegisterHelpers(container);
        }

        private static void RegisterConfiguration(IObjectContainer container, GlobalSettings globalSettings)
        {
            container.RegisterInstanceAs(globalSettings);
        }

        private static void RegisterServices(IObjectContainer container, GlobalSettings globalSettings)
        {
            var accessTokenProvider = new AccessTokenProvider(globalSettings);

            var servicesDriver = new ServicesDriver();
            container.RegisterInstanceAs(servicesDriver);

            // SpotifyService
            var spotifyService = new SpotifyPlaylistsService(globalSettings, accessTokenProvider);
            container.RegisterInstanceAs(spotifyService);
        }

        private static void RegisterHelpers(IObjectContainer container)
        {
            container.RegisterInstanceAs(new Faker());
        }

    }
}
