using Default.WebApp.Template.Server.Controllers;

namespace Default.WebApp.Template.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(AccountController).GetConstructors());
    }
}