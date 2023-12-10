using Default.WebApp.Template.Domain.Enums;
using Default.WebApp.Template.Domain.Models;
using Default.WebApp.Template.Infrastrucutre.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Default.WebApp.Template.Application.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, CommandStatus>
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public RegisterUserHandler(ApplicationDbContext context, UserManager<User> userManager)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(userManager);

            this.context = context;
            this.userManager = userManager;
        }

        public async Task<CommandStatus> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await context.Users.AnyAsync(user => user.Email == request.Email, cancellationToken);

            if (userExists)
                return CommandStatus.Failed("Utilizatorul deja exista");

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.Username,
                Email = request.Email
            };

            var createResult = await userManager.CreateAsync(user, request.Password);

            IdentityResult roleResult;

            roleResult = await userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            if (!roleResult.Succeeded || !createResult.Succeeded)
                return CommandStatus.Failed("Utilizatorul nu a putut fi inregistrat");

            return new CommandStatus();
        }
    }
}