using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlogApp.Business;
using BlogApp.Data;
using BlogApp.Data.Repository;

namespace BlogApp.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogAppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
        }

        public static void RegisterServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddScoped<AuthorBusiness>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<BlogAppDbContext>();
        }
    }
}