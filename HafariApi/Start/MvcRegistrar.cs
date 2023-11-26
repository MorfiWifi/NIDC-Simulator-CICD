using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace Api.Start
{
    internal class DoubleInfinityConverter : JsonConverter<double>
    {
        public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.GetDouble();

        public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                writer.WriteStringValue(default(double).ToString());
                return;
            }
            writer.WriteStringValue(value.ToString());
        }
    }
    public static class MvcRegistrar
    {
        public static void RegisterMvc(this IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new DoubleInfinityConverter())
        ); 
        }

        public static void ConfigureMvc(this IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(x => x
             .WithOrigins("https://static.iran.liara.ir")
             .WithOrigins("https://static-panel.iran.liara.run")
             .WithMethods("GET", "POST" , "PUT" , "DELETE" , "PATCH")
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true)
             .AllowCredentials()
             .SetPreflightMaxAge(TimeSpan.FromDays(1))
             );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            var provider = new FileExtensionContentTypeProvider();
            //provider.Mappings[".apk"] = "application/vnd.android.package-archive";
            app.UseStaticFiles();
            app.UseStaticFiles( new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                {
                    { ".glb", "model/gltf-binary" }, // Add MIME type for .glb files
                    // Add other MIME types if required
                })
            });
        }
    }
}
