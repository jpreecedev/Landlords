namespace Landlords.Core
{
    using System;
    using Database;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Model;
    using Model.Database;
    using System.Threading;
    using System.Collections.Generic;

    public static class SeedExtensions
    {
        public static void Seed(this LLDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
            var userManager = serviceProvider.GetService<ApplicationUserManager>();

            AsyncHelpers.RunSync(() => CreateRoles(context, roleManager));
            AsyncHelpers.RunSync(() => CreateUsers(context, userManager));
        }

        private static async Task CreateRoles(LLDbContext context, ApplicationRoleManager roleManager)
        {
            if (!await context.Roles.AnyAsync())
            {
                foreach (var role in ApplicationRoles.AllRoles)
                {
                    await roleManager.CreateAsync(new ApplicationRole(role));
                }
            }
        }

        private static async Task CreateUsers(LLDbContext context, ApplicationUserManager userManager)
        {
            if (!await userManager.Users.AnyAsync(c => c.UserName == "admin@landlords.com"))
            {
                var administrator = new ApplicationUser
                {
                    UserName = "admin@landlords.com",
                    Email = "admin@landlords.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                await userManager.CreateAsync(administrator, "Password123");
                await userManager.AddToRoleAsync(administrator, ApplicationRoles.SiteAdministrator);
                await userManager.SetUserPermissionsAsync(administrator.Id, Permissions.DefaultAdministratorPermissions);
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "jonpreece@hotmail.co.uk"))
            {
                var landlord = new ApplicationUser
                {
                    UserName = "jonpreece@hotmail.co.uk",
                    Email = "jonpreece@hotmail.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Jon",
                    LastName = "Preece"
                };

                await userManager.CreateAsync(landlord, "Password123");
                await userManager.AddToRoleAsync(landlord, ApplicationRoles.Landlord);
                await userManager.SetUserPermissionsAsync(landlord.Id, Permissions.DefaultLandlordPermissions);

                var portfolio = new Portfolio
                {
                    Created = DateTime.Now,
                    DisplayName = "Jons Portfolio",
                    Name = "JonPreece-ABC123"
                };
                await context.Portfolios.AddAsync(portfolio);
                await context.SaveChangesAsync();

                var link = new ApplicationUserPortfolio
                {
                    Created = DateTime.Now,
                    PortfolioId = portfolio.Id,
                    UserId = landlord.Id
                };
                await context.ApplicationUserPortfolios.AddAsync(link);
                await context.SaveChangesAsync();
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "landlord@hotmail.co.uk"))
            {
                var landlord = new ApplicationUser
                {
                    UserName = "landlord@hotmail.co.uk",
                    Email = "landlord@hotmail.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Land",
                    LastName = "Lord"
                };

                await userManager.CreateAsync(landlord, "Password123");
                await userManager.AddToRoleAsync(landlord, ApplicationRoles.Landlord);
                await userManager.SetUserPermissionsAsync(landlord.Id, Permissions.DefaultLandlordPermissions);

                var portfolio = new Portfolio
                {
                    Created = DateTime.Now,
                    DisplayName = "Landlord Portfolio",
                    Name = "LandLord-ASD432"
                };
                await context.Portfolios.AddAsync(portfolio);
                await context.SaveChangesAsync();

                var link = new ApplicationUserPortfolio
                {
                    Created = DateTime.Now,
                    PortfolioId = portfolio.Id,
                    UserId = landlord.Id
                };
                await context.ApplicationUserPortfolios.AddAsync(link);
                await context.SaveChangesAsync();
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "accountant@hotmail.co.uk"))
            {
                var accountant = new ApplicationUser
                {
                    UserName = "accountant@hotmail.co.uk",
                    Email = "accountant@hotmail.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Account",
                    LastName = "Ant"
                };

                await userManager.CreateAsync(accountant, "Password123");
                await userManager.AddToRoleAsync(accountant, ApplicationRoles.Accountant);
                await userManager.SetUserPermissionsAsync(accountant.Id, Permissions.DefaultAccountantPermissions);

                var portfolio = await context.Portfolios.FirstAsync(c => c.Name == "JonPreece-ABC123");

                var link = new ApplicationUserPortfolio
                {
                    Created = DateTime.Now,
                    PortfolioId = portfolio.Id,
                    UserId = accountant.Id
                };

                await context.ApplicationUserPortfolios.AddAsync(link);
                await context.SaveChangesAsync();
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "other@hotmail.co.uk"))
            {
                var accountant = new ApplicationUser
                {
                    UserName = "other@hotmail.co.uk",
                    Email = "other@hotmail.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Other",
                    LastName = "User"
                };

                await userManager.CreateAsync(accountant, "Password123");
                await userManager.AddToRoleAsync(accountant, ApplicationRoles.OtherUser);
                await userManager.SetUserPermissionsAsync(accountant.Id, Permissions.DefaultOtherUserPermissions);

                var portfolio = await context.Portfolios.FirstAsync(c => c.Name == "JonPreece-ABC123");

                var link = new ApplicationUserPortfolio
                {
                    Created = DateTime.Now,
                    PortfolioId = portfolio.Id,
                    UserId = accountant.Id
                };

                await context.ApplicationUserPortfolios.AddAsync(link);
                await context.SaveChangesAsync();
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "administrator@agency.co.uk"))
            {
                var accountant = new ApplicationUser
                {
                    UserName = "administrator@agency.co.uk",
                    Email = "administrator@agency.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Agency",
                    LastName = "Administrator"
                };

                await userManager.CreateAsync(accountant, "Password123");
                await userManager.AddToRoleAsync(accountant, ApplicationRoles.AgencyAdministrator);
                await userManager.SetUserPermissionsAsync(accountant.Id, Permissions.DefaultAgencyAdministratorPermissions);
            }

            if (!await userManager.Users.AnyAsync(c => c.UserName == "user@agency.co.uk"))
            {
                var accountant = new ApplicationUser
                {
                    UserName = "user@agency.co.uk",
                    Email = "user@agency.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Agency",
                    LastName = "User"
                };

                await userManager.CreateAsync(accountant, "Password123");
                await userManager.AddToRoleAsync(accountant, ApplicationRoles.AgencyAdministrator);
                await userManager.SetUserPermissionsAsync(accountant.Id, Permissions.DefaultAgencyAdministratorPermissions);
            }

        }

        private static class AsyncHelpers
        {
            /// <summary>
            /// Execute's an async Task<T> method which has a void return value synchronously
            /// </summary>
            /// <param name="task">Task<T> method to execute</param>
            public static void RunSync(Func<Task> task)
            {
                var oldContext = SynchronizationContext.Current;
                var synch = new ExclusiveSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(synch);
                synch.Post(async _ =>
                {
                    try
                    {
                        await task();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                }, null);
                synch.BeginMessageLoop();

                SynchronizationContext.SetSynchronizationContext(oldContext);
            }

            /// <summary>
            /// Execute's an async Task<T> method which has a T return type synchronously
            /// </summary>
            /// <typeparam name="T">Return Type</typeparam>
            /// <param name="task">Task<T> method to execute</param>
            /// <returns></returns>
            public static T RunSync<T>(Func<Task<T>> task)
            {
                var oldContext = SynchronizationContext.Current;
                var synch = new ExclusiveSynchronizationContext();
                SynchronizationContext.SetSynchronizationContext(synch);
                T ret = default(T);
                synch.Post(async _ =>
                {
                    try
                    {
                        ret = await task();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                }, null);
                synch.BeginMessageLoop();
                SynchronizationContext.SetSynchronizationContext(oldContext);
                return ret;
            }

            private class ExclusiveSynchronizationContext : SynchronizationContext
            {
                private bool done;
                public Exception InnerException { get; set; }
                readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
                readonly Queue<Tuple<SendOrPostCallback, object>> items =
                    new Queue<Tuple<SendOrPostCallback, object>>();

                public override void Send(SendOrPostCallback d, object state)
                {
                    throw new NotSupportedException("We cannot send to our same thread");
                }

                public override void Post(SendOrPostCallback d, object state)
                {
                    lock (items)
                    {
                        items.Enqueue(Tuple.Create(d, state));
                    }
                    workItemsWaiting.Set();
                }

                public void EndMessageLoop()
                {
                    Post(_ => done = true, null);
                }

                public void BeginMessageLoop()
                {
                    while (!done)
                    {
                        Tuple<SendOrPostCallback, object> task = null;
                        lock (items)
                        {
                            if (items.Count > 0)
                            {
                                task = items.Dequeue();
                            }
                        }
                        if (task != null)
                        {
                            task.Item1(task.Item2);
                            if (InnerException != null) // the method threw an exeption
                            {
                                throw new AggregateException("AsyncHelpers.Run method threw an exception.", InnerException);
                            }
                        }
                        else
                        {
                            workItemsWaiting.WaitOne();
                        }
                    }
                }

                public override SynchronizationContext CreateCopy()
                {
                    return this;
                }
            }
        }
    }
}