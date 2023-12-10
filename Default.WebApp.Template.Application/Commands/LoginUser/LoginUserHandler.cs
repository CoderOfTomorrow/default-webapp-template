using Default.WebApp.Template.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Default.WebApp.Template.Application.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, CommandStatus>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginUserHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            ArgumentNullException.ThrowIfNull(userManager);
            ArgumentNullException.ThrowIfNull(signInManager);

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<CommandStatus> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            if (user is null)
                return CommandStatus.Failed("The user doesn't exist.");

            var passwordStatus = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!passwordStatus.Succeeded)
                return CommandStatus.Failed("Wrong password.");

            return new();
        }
    }
}