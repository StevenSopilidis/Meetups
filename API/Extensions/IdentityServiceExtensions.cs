using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //configure idenity Service
            services.AddIdentityCore<AppUser>(opt => {
                opt.Password.RequireNonAlphanumeric= false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            //configure the authentication 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey= true,
                        IssuerSigningKey = key,
                        ValidateAudience= false,
                        ValidateIssuer= false
                    };

                    //for signalR
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context => {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.Request.Path;
                            if(!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(opt => {
                opt.AddPolicy("IsActivityHost", policy => {
                    policy.Requirements.Add(new IsHostRequirment());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsHostRequirmentHandler>();
            services.AddScoped<TokenService>();
            return services;
        }
    }
}