using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;
using WebApi.Persistences.Contexts;
using WebApi.Persistences.Repositories;
using WebApi.Services;
using WebApi.Extensions;
using WebApi.Domain.Security.Hashing;
using WebApi.Security.Hashing;
using WebApi.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain.Models;
using WebApi.Domain.Communication;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase("web-api-database");
            });

            services.AddCustomSwagger();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<Domain.Security.Token.ITokenHandler, Security.Tokens.TokenHandler>();

            services.AddScoped<ICategoryRespository<Category>, CategoryRepository>();
            services.AddScoped<ICategoryService<Category, CategoryResponse>, CategoryService>();

            services.AddScoped<IProductRepository<Product>, ProductRepository>();
            services.AddScoped<IProductService<Product, ProductResponse>, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.SecurityKey,
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseCustomSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
