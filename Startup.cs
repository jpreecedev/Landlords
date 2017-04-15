namespace Landlords
{
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Data;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using IdentityServer4.Models;
    using System.Collections.Generic;

    public class Startup
    {
        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var sqlConnectionString = Configuration.GetConnectionString("LLDbContext");
            services.AddDbContext<LLDbContext>(options => options.UseSqlServer(sqlConnectionString));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<LLDbContext>();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddOperationalStore(builder => builder.UseSqlServer(sqlConnectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationStore(builder => builder.UseSqlServer(sqlConnectionString, options => options.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<IdentityUser>()
                .AddTemporarySigningCredential();
            
            services.AddMvc();

            services.AddScoped<IDataAccessProvider, DataAccessProvider>();
            services.AddScoped<IDataContext, LLDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => builder.WithOrigins("http://localhost:8080", "chrome-extension://fhbjgbiflinjbdggehcddcbncdddomop").AllowAnyHeader().AllowAnyMethod());

            //app.UseJwtBearerAuthentication(new JwtBearerOptions()
            //{
            //    Audience = "http://localhost:52812/resources",
            //    Authority = "http://localhost:52812/",
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = true,
            //    RequireHttpsMetadata = false
            //});

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                ClientId = "openIdConnectClient",
                Authority = "https://localhost:44350/",
                SignInScheme = "cookie"
            });

            InitializeDbTestData(app);

            app.UseIdentity();
            app.UseIdentityServer();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        private static void InitializeDbTestData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
                scope.ServiceProvider.GetRequiredService<LLDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.Clients.Any())
                {
                    foreach (var client in Identity.Clients.Get())
                        context.Clients.Add(client.ToEntity());
                    context.SaveChanges();
                }

                //if (!context.IdentityResources.Any())
                //{
                //    foreach (var resource in Resources.GetIdentityResources())
                //        context.IdentityResources.Add(resource.ToEntity());
                //    context.SaveChanges();
                //}

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Identity.ApiResources.GetApiResources())
                        context.ApiResources.Add(resource.ToEntity());
                    context.SaveChanges();
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                if (!userManager.Users.Any())
                    foreach (var inMemoryUser in Identity.Users.Get())
                    {
                        var identityUser = new IdentityUser(inMemoryUser.Username)
                        {
                            Id = inMemoryUser.SubjectId
                        };

                        foreach (var claim in inMemoryUser.Claims)
                            identityUser.Claims.Add(new IdentityUserClaim<string>
                            {
                                UserId = identityUser.Id,
                                ClaimType = claim.Type,
                                ClaimValue = claim.Value
                            });

                        userManager.CreateAsync(identityUser, "Password123!").Wait();
                    }
            }
        }
    }

    //internal class Resources
    //{
    //    public static IEnumerable<IdentityResource> GetIdentityResources()
    //    {
    //        return new List<IdentityResource>
    //        {
    //            new IdentityResources.OpenId(),
    //            new IdentityResources.Profile(),
    //            new IdentityResources.Email(),
    //            new IdentityResource
    //            {
    //                Name = "role",
    //                UserClaims = new List<string> {"role"}
    //            }
    //        };
    //    }


    //}

}