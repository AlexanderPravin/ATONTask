namespace Infrastructure
{
    /// <summary>
    /// Provides extension methods for the IServiceCollection interface to add infrastructure services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds infrastructure services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <param name="configuration">The IConfiguration to get data</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PSQSQL"));
            });

            // Adds the UserRepository to the service collection
            services.AddScoped<IRepository<User>, UserRepository>();
            
            // Adds the UnitOfWork to the service collection
            services.AddScoped<UnitOfWork>();

            return services;
        }
    }
}