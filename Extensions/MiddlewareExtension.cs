using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions
{
    public static class MiddlewareExtension
    {
		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(cfg =>
			{
				cfg.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "JWT API",
					Version = "v3",
					Description = "Api conçu en ASP.NET 5 Core",
					Contact = new OpenApiContact
					{
						Name = "François-Pierre ROUSSEAU",
					},
					License = new OpenApiLicense
					{
						Name = "MIT",
					},
				});

				cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "JSON Web Token to access resources. Example: Bearer {token}",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
						},
						new [] { string.Empty }
					}
				});

				//	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				//	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				//	cfg.IncludeXmlComments(xmlPath);
			});

			return services;
		}

		public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger().UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API");
				options.DocumentTitle = "JWT API";
			});

			return app;
		}
	}
}
