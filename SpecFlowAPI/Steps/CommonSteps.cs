using System.Net;
using TechTalk.SpecFlow;

namespace SpecFlowAPI.Steps
{
    [Binding]
    public class CommonSteps : ServiceStepsBase
    {
        public CommonSteps(Helper helper, ServicesDriver servicesDriver) : base(helper, servicesDriver)
        {
        }

        #region GIVEN
        #endregion

        #region THEN

        [Then(@"the status code should be '(.*)'")]
        public void ThenTheStatusCodeShouldBe(HttpStatusCode statusCode)
        {
            ResponseStatusCodeIs(statusCode);
        }

        #endregion

        #region THEN
        #endregion
    }
}
