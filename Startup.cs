namespace Landlords
{
    using System;
    using System.Text;
    using Core;
    using Database;
    using Jwt;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Model.Database;
    using Services;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddCors();

            var sqlConnectionString = Configuration.GetConnectionString("LLDbContext");
            services.AddDbContext<LLDbContext>(options => options.UseSqlServer(sqlConnectionString));

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN")
                .RegisterDI()
                .AddOptions()
                .Configure<JwtConfiguration>(Configuration.GetSection("Jwt"))
                .Configure<EmailConfiguration>(Configuration.GetSection("Email"));

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options => { options.Password.RequireNonAlphanumeric = false; })
                .AddEntityFrameworkStores<LLDbContext, Guid>()
                .AddUserManager<ApplicationUserManager>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
                              IOptions<JwtConfiguration> jwtConfiguration, IAntiforgery antiforgery)
        {
#if DEBUG
            app.UseCors(builder => builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());
#endif

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LLDbContext>();
                context.Database.Migrate();
              
            }

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Value.Secret));

            app.UseJwt(jwtConfiguration, signingKey)
                .UseXsrf(antiforgery)
                .UseMiddleware<TokenProviderMiddleware>(Options.Create(new TokenProviderOptions
                {
                    Audience = jwtConfiguration.Value.Audience,
                    Issuer = jwtConfiguration.Value.Issuer,
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                }));

            app.UseMvc();
        }
    }
}