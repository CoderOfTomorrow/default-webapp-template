using Default.WebApp.Template.Application.Commands.RegisterUser;

namespace Default.WebApp.Template.Server.Composition
{
    public static class PresentationModule
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.RegisterServicesFromAssembly(typeof(RegisterUserHandler).Assembly);
            });

            return services;
        }
    }
}