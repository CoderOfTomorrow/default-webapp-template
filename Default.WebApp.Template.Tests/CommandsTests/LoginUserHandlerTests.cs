using Default.WebApp.Template.Application.Commands.LoginUser;

namespace Default.WebApp.Template.Tests.CommandsTests
{
    public class LoginUserHandlerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(LoginUserHandler).GetConstructors());
    }
}