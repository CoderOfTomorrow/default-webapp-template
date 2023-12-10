using MediatR;

namespace Default.WebApp.Template.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<CommandStatus>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}