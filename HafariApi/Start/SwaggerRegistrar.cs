using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Api.Start
{
    public static class SwaggerRegistrar
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.CustomSchemaIds(i => i.FullName);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        }, new List<string>()
                }});

                c.OperationFilter<AddRequiredHeaderParameter>();

            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        { 
            
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "API V1.0");
                c.DocumentTitle = "Api";
                c.EnableDeepLinking();
                c.EnableValidator();
            });
        }
    }
}
