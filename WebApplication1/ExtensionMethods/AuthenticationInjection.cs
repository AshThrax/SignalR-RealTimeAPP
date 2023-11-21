using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApplication1.Policy;

namespace WebApplication1.ExtensionMethods
{
    public static class AuthenticationInjection
    {
        public static IServiceCollection AddAuthZero(IServiceCollection services,IConfiguration Config)
        {
            var domain = $"https://{Config["Auth0:Dommain"]}/";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                { 
                    options.Authority = domain ;
                    options.Audience = Config["Auth0:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier

                    };
                });
            services.AddAuthorization(options => 
            {
                options.AddPolicy("read:message", policy => policy.Requirements.Add(new HasScopeRequirement("read:message",domain)));
            });
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
             

             
            return services;
        }
    }
}
