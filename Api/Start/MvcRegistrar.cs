using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Api.Start
{
    public static class MvcRegistrar
    {
        public static void RegisterMvc(this IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
        }

        public static void ConfigureMvc(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(x => x
             .WithOrigins("https://imasdk.googleapis.com")
             .WithMethods("GET", "POST")
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true)
             .AllowCredentials()
             .SetPreflightMaxAge(TimeSpan.FromDays(1))
             );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            var provider = new FileExtensionContentTypeProvider();
            //provider.Mappings[".apk"] = "application/vnd.android.package-archive";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
        }
    }
}
