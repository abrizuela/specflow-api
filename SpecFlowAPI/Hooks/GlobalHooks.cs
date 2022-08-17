using System;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowAPI
{
    [Binding]
    public class GlobalHooks
    {
        private static readonly GlobalSettings GlobalSettings;
        static GlobalHooks()
        {
            GlobalSettings = CreateConfiguration();
        }

        private static GlobalSettings CreateConfiguration()
        {
			var environmentName = Environment.GetEnvironmentVariable("Spotify__Environment") ?? "local";

			var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true) //load environment settings
                .AddEnvironmentVariables()
                .Build()
                .Get<GlobalSettings>();

            return config;
        }

        [BeforeScenario]
        public static void CreateConfig(ScenarioContext context, FeatureInfo featureInfo)
        {
            DependencyInjection.ConfigureDependencies(context.ScenarioContainer, GlobalSettings);
        }

        [AfterScenario]
        public static void ShowResponseError(ScenarioContext context, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            var driver = context.ScenarioContainer.Resolve<ServicesDriver>();
            var response = driver.ServiceResponse;

            if (response is not null && context.ScenarioExecutionStatus.Equals(ScenarioExecutionStatus.TestError))
            {
                specFlowOutputHelper.WriteLine($"Last API Run: {response.GetProblemDetails()}");
            }
        }
    }
}
