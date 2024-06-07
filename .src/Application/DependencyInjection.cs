namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<JwtTokenHelper>();
        
        services.AddScoped<UserService>();

        return services;
    }
}