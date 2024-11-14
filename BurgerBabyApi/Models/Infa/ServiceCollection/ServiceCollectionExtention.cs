using BurgerBabyApi.Models.EFModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication1.ServiceCollection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "BurgerBabyApi",
                    ValidAudience = "BurgerBaby",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSecretKey"]))
                };
            });
            return services;
        }

        public static IServiceCollection AddRefreshTokenAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
        .AddCookie("RefreshTokenScheme", options =>
        {
            options.Cookie.Name = "RefreshToken";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite=SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.LoginPath = "/Home/Login";
            options.AccessDeniedPath = "/Home/Error";
        });
            return services;
        }
    }
}
