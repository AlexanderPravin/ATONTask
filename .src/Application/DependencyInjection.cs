using Application.Validators;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<UserValidator>();
        
        services.AddScoped<JwtTokenHelper>();
        
        services.AddScoped<UserService>();

        return services;
    }
}