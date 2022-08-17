using FluentAssertions;
using RestSharp;
using System.Net;

namespace SpecFlowAPI
{
    public abstract class ServiceStepsBase
    {
        public Helper Helper { get; }
        public ServicesDriver Driver { get; set; }
        public IRestResponse? Response
        {
            get => Driver.ServiceResponse;
            set => Driver.ServiceResponse = value;
        }

        public ServiceStepsBase(Helper helper, ServicesDriver driver)
        {
            Driver = driver;
            Helper = helper;
        }

        /// <summary>
        /// Validates that the given response has the given status code
        /// </summary>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        public static void StatusCodeIs(IRestResponse response, HttpStatusCode statusCode)
        {
            _ = response.StatusCode.Should().Be(statusCode);
        }

        /// <summary>
        /// Validates that the global property Response has the given status code
        /// </summary>
        /// <param name="statusCode"></param>
        public void ResponseStatusCodeIs(HttpStatusCode statusCode)
        {
            StatusCodeIs(Response!, statusCode);
        }
    }
}
