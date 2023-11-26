using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Db;
using Core.Repository;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Api.Start
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PanakDbContext>( options => options.UseSqlServer(configuration.GetConnectionString("PanakContext")),ServiceLifetime.Transient);
            services.AddScoped<DbContext, PanakDbContext>();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.Scan(scan => scan
                   .FromAssemblies(typeof(Services.Identity.IAuthService).GetTypeInfo().Assembly)
                   .AddClasses()
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
