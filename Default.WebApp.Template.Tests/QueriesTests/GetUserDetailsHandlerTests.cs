using Default.WebApp.Template.Application.Queries.GetUserDetails;

namespace Default.WebApp.Template.Tests.QueriesTests
{
    public class GetUserDetailsHandlerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(GetUserDetailsHandler).GetConstructors());
    }
}