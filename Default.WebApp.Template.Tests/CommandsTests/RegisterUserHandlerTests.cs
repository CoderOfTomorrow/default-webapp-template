using Default.WebApp.Template.Application.Commands.RegisterUser;

namespace Default.WebApp.Template.Tests.CommandsTests
{
    public class RegisterUserHandlerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(RegisterUserHandler).GetConstructors());
    }
}