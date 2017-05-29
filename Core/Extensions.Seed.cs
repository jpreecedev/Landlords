﻿namespace Landlords.Core
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
    using System.Linq;
    using Permissions;
    using System.Reflection;
    using Model.DataTypes;

    public static class SeedExtensions
    {
        public static void Seed(this LLDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
            var userManager = serviceProvider.GetService<ApplicationUserManager>();

            CreatePermissions(context);
            AsyncHelpers.RunSync(() => CreateEmails(context));
            AsyncHelpers.RunSync(() => CreateRoles(context, roleManager));
            AsyncHelpers.RunSync(() => CreateUsers(context, userManager));
            AsyncHelpers.RunSync(() => CreateAgencies(context));
            AsyncHelpers.RunSync(() => CreateChecklists(context, userManager));
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
                await userManager.SetUserPermissionsAsync(administrator.Id, DefaultPermissions.Administrator.Select(c => Guid.Parse(c)).ToArray());
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
                await userManager.SetUserPermissionsAsync(landlord.Id, DefaultPermissions.Landlord.Select(c => Guid.Parse(c)).ToArray());

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
                await userManager.SetUserPermissionsAsync(landlord.Id, DefaultPermissions.Landlord.Select(c => Guid.Parse(c)).ToArray());

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
                await userManager.SetUserPermissionsAsync(accountant.Id, DefaultPermissions.Accountant.Select(c => Guid.Parse(c)).ToArray());

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
                await userManager.SetUserPermissionsAsync(accountant.Id, DefaultPermissions.OtherUser.Select(c => Guid.Parse(c)).ToArray());

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
                await userManager.SetUserPermissionsAsync(accountant.Id, DefaultPermissions.AgencyAdministrator.Select(c => Guid.Parse(c)).ToArray());
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
                await userManager.SetUserPermissionsAsync(accountant.Id, DefaultPermissions.AgencyAdministrator.Select(c => Guid.Parse(c)).ToArray());
            }
        }

        private static async Task CreateAgencies(LLDbContext context)
        {
            if (!await context.Agencies.AnyAsync(c => c.Name == "Entwhistle Green"))
            {
                var agency = new Agency
                {
                    Created = DateTime.Now,
                    Name = "Entwhistle Green"
                };

                await context.Agencies.AddAsync(agency);
                await context.SaveChangesAsync();

                var user1 = await context.Users.FirstAsync(c => c.UserName == "administrator@agency.co.uk");
                var user2 = await context.Users.FirstAsync(c => c.UserName == "user@agency.co.uk");

                user1.AgencyId = agency.Id;
                user2.AgencyId = agency.Id;

                await context.SaveChangesAsync();

                var landlord1 = await context.Users.FirstAsync(c => c.UserName == "jonpreece@hotmail.co.uk");
                var portfolios = context.ApplicationUserPortfolios.Where(c => c.UserId == landlord1.Id);

                await portfolios.ForEachAsync(c => c.AgencyId = agency.Id);
                await context.SaveChangesAsync();
            }
        }

        private static void CreatePermissions(LLDbContext context)
        {
            typeof(PermissionAttribute).GetTypeInfo().Assembly
                .GetTypes()
                .SelectMany(c => c.GetMethods(BindingFlags.Instance | BindingFlags.Public))
                .Where(c => c.CustomAttributes.Any(d => d.AttributeType == typeof(PermissionAttribute)))
                .Select(c => c.GetCustomAttribute<PermissionAttribute>())
                .ToList()
                .ForEach(permissionAttribute =>
                {
                    if (!context.Permissions.Any(c => c.Id == permissionAttribute.PermissionId))
                    {
                        context.Permissions.Add(new Permission
                        {
                            Id = permissionAttribute.PermissionId,
                            Description = permissionAttribute.Description,
                            RouteId = permissionAttribute.RouteId
                        });
                    }
                });

            context.SaveChanges();
        }

        private static async Task CreateEmails(LLDbContext context)
        {
            if (!await context.EmailTemplates.AnyAsync(c => c.Key == "ResendVerification"))
            {
                await context.EmailTemplates.AddAsync(new EmailTemplate
                {
                    Key = "ResendVerification",
                    Description = "Re-send verification email",
                    Created = DateTime.Now,
                    Subject = "Confirm email",
                    Body = "Click this <a href=\"http://localhost:52812/api/register/confirmemail?userId={0}&code={1}\">link</a> mofo."
                });
            }
            if (!await context.EmailTemplates.AnyAsync(c => c.Key == "Register"))
            {
                await context.EmailTemplates.AddAsync(new EmailTemplate
                {
                    Key = "Register",
                    Description = "Registration confirmation",
                    Created = DateTime.Now,
                    Subject = "Confirm email",
                    Body = "Click this <a href=\"http://localhost:52812/api/register/confirmemail?userId={0}&code={1}\">link</a> mofo."
                });
            }

            await context.SaveChangesAsync();
        }

        private static async Task CreateChecklists(LLDbContext context, ApplicationUserManager userManager)
        {
            var admin = await context.Users.FirstAsync(c => c.UserName == "admin@landlords.com");

            if (!await context.Checklists.AnyAsync(c => c.Name == "New tenant move in"))
            {
                var moveInChecklist = new Checklist
                {
                    Created = DateTime.Now,
                    Name = "New tenant move in",
                    Image = "checklist.png",
                    Description = "For when a new tenant is moving into the property",
                    IsAvailableDownstream = true,
                    IsPropertyMandatory = true,
                    UserId = admin.Id
                };

                await context.Checklists.AddAsync(moveInChecklist);
                await context.SaveChangesAsync();

                var checklistItems = new List<ChecklistItem>
                {
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Determine rental amount",
                        IsExpanded = true,
                        Key = "NL001",
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Take photos (or get a professional to do it)",
                        Key = "NL002",
                        Template = ChecklistTemplates.DocumentUpload
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Advertise the property",
                        Key = "NL003",
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Conduct viewings",
                        Key = "NL004",
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Negotiate & accept offer",
                        Key = "NL005",
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Take fees",
                        Key = "NL006",
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Get proof of Id",
                        Key = "NL007",
                        Template = ChecklistTemplates.DocumentUpload
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Establish right to rent",
                        Key = "NL008",
                        Template = ChecklistTemplates.CommentsAndDateOfAction
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Conduct reference checks",
                        Key = "NL009",
                        Template = ChecklistTemplates.DateOfAction
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Ask for a guarantor if necessary",
                        Key = "NL010",
                        Template = ChecklistTemplates.CommentsOnly
                    }
                };

                await context.ChecklistItems.AddRangeAsync(checklistItems);
            }
            
            await context.SaveChangesAsync();
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