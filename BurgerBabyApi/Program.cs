using BurgerBabyApi.Models.EFModel;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using BurgerBabyApi.Models.Services.Interface;
using BurgerBabyApi.Models.Services;
using BurgerBabyApi.Models.Repo.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using BurgerBabyApi.Models.Infa;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using BurgerBabyApi.Models.Infa.ServiceCollection;
using WebApplication1.ServiceCollection;
using Microsoft.AspNetCore.Authorization;

namespace BurgerBabyApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BurgerBabyApiContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddService();
            builder.Services.AddRepo();

           
         
            builder.Services.AddRefreshTokenAuthentication();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
              
            });



            builder.Services.AddCors(
                options => options.AddPolicy(
                    "MemberPolicy", builder =>
                    {
                        builder.WithOrigins("http://localhost:7266", "http://localhost:8082").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
                    }
                    )
                );

            var app = builder.Build();
            app.UseCors("MemberPolicy");

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.GetFullPath(Directory.GetParent(Directory.GetCurrentDirectory()) + "\\BurgerBaby\\wwwroot\\savedata"))
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}