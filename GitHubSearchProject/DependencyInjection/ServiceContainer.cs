
namespace GitHubSearchProject.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add JWT authentication scheme
            JWTAuthentication.AddJWTAuthenticationScheme(services, config);
            JWTAuthentication.AddAuthorizationPolicies(services);
            return services;       
        }       
    }
}
