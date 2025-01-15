using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace GitHubSearchProject.DependencyInjection
{
    public static class JWTAuthentication
    {
        public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration configuration)
        {
            // add JWT service
            var _authkey = configuration.GetValue<string>("JwtSettings:securitykey");
            services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.RequireHttpsMetadata = false;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authkey)),
                    ClockSkew = TimeSpan.Zero
                };
            });           
            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            // Add authorization policies (optional, for finer control)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUsersOnly", policy =>
                    policy.RequireAuthenticatedUser());
            });
            return services;
        }
    }
}
