using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Start
{
    public static class VersioningRegistrar
    {
        public static void RegisterVersioning(this IServiceCollection services)
        {

            services.AddApiVersioning(c =>
            {
                c.ReportApiVersions = true;
                c.DefaultApiVersion = new ApiVersion(1, 0);
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });
        }

        public static void ConfigureVersioning(this IApplicationBuilder app)
        {
            app.UseApiVersioning();
        }
    }
}
