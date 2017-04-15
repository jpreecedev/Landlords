namespace Landlords
{
    using Landlords.Jwt;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;
    using Database;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
            services.AddMvc();
            services.AddCors();

            var sqlConnectionString = Configuration.GetConnectionString("LLDbContext");
            services.AddDbContext<LLDbContext>(options => options.UseSqlServer(sqlConnectionString));

            services.AddScoped<IDataAccessProvider, DataAccessProvider>();
            
            services.AddOptions();
            services.Configure<JwtConfiguration>(Configuration.GetSection(("Jwt")));

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<LLDbContext, Guid>()
                .AddUserManager<ApplicationUserManager>()
                .AddUserStore<ApplicationUserStore>()
                .AddDefaultTokenProviders();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<JwtConfiguration> jwtConfiguration)
        {
#if DEBUG
            app.UseCors(builder => builder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());
#endif

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<LLDbContext>().Database.Migrate();
            }

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfiguration.Value.Secret));
            
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = GetTokenValidationParameters(jwtConfiguration, signingKey)
            });

            var options = new TokenProviderOptions
            {
                Audience = jwtConfiguration.Value.Audience,
                Issuer = jwtConfiguration.Value.Issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
            app.UseMvc();
        }

        private static TokenValidationParameters GetTokenValidationParameters(IOptions<JwtConfiguration> jwtConfiguration, SymmetricSecurityKey signingKey)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = jwtConfiguration.Value.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfiguration.Value.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}