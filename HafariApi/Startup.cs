using Api.Start;
using AutoMapper;
using HafariApi;
using HafariApi.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.RegisterDependencies(Configuration);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.RegisterMvc();
            services.RegisterAuthentication(Configuration);
            var floatConverter = new LawAbidingFloatConverter();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(10);
                hubOptions.MaximumReceiveMessageSize = 102400; // bytes


            });
            services.RegisterVersioning();
            services.RegisterSwagger();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMemoryCache();
            services.AddHostedService<RedisUpdaterService>();
            services.AddHostedService<HubUpdaterService>();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add static files to the request pipeline e.g. hello.html or world.css.
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseRouting();
            app.ConfigureMvc(env);
            app.ConfigureSwagger();
            app.ConfigureAuthentication();
            app.UseCors(x => x.AllowAnyOrigin());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();                
                endpoints.MapHub<SimulationHub>("/SimulationHub");
                endpoints.MapHub<ChatHub>("/ChatHub");
            });

        }
    }
}
