namespace Landlords
{
    using System;
    using Core;
    using Database;
    using Jwt;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Model.Database;
    using Services;
    using Landlords.Notifications;
    using Microsoft.AspNetCore.Identity;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BearerTokensOptions>(options => Configuration.GetSection("BearerTokens").Bind(options));
            services.RegisterDI();

            services.AddDbContext<LLDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LLDbContext"),
                    optionsBuilder =>
                    {
                        optionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                        optionsBuilder.EnableRetryOnFailure();
                    });
            });
            
            services.AddAuthorization(config => config.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

            services.UseJwt(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN")
                .AddOptions()
                .Configure<EmailConfiguration>(Configuration.GetSection("Email"));
            
            services.AddWebSocketManager();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options => { options.Password.RequireNonAlphanumeric = false; })
                .AddEntityFrameworkStores<LLDbContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<ApplicationUserManager>()
                .AddUserStore<ApplicationUserStore>()
                .AddRoleManager<ApplicationRoleManager>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider, IAntiforgery antiforgery)
        {
            app.UseTokenExceptionHandler()
                .UseWebSockets();

            app.MapWebSocketManager("/notifications", serviceProvider.GetService<NotificationsMessageHandler>());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseAuthentication()
                .UseXsrf(antiforgery);

            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializerService>();
                dbInitializer.Initialize();
                dbInitializer.SeedData();
            }
            
            app.UseMvc();
        }
    }
}