using System.Collections;
using BurgerBabyApi.Models.Repo.Interface;
using BurgerBabyApi.Models.Services.Interface;
using BurgerBabyApi.Models.Services;
using BurgerBabyApi.Models.Repo;

namespace BurgerBabyApi.Models.Infa.ServiceCollection
{
    public static class EntityCollectionExt
    {
        public static IServiceCollection AddRepo( this IServiceCollection services) {
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IProductRepository, AdoProductRepository>();
            services.AddScoped<IImgRepository, ImgRepository>();
            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImgService, ImgService>();
            return services;
        }
    }
}
