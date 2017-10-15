namespace Landlords.Core
{
    using System;
    using Database;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;
    using System.Threading;
    using System.Collections.Generic;
    using System.Linq;
    using Permissions;
    using System.Reflection;
    using Model.DataTypes;
    using Model.Entities;

    public static class SeedExtensions
    {
        public static void SeedDatabase(this LLDbContext context, ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            CreatePermissions(context);
            AsyncHelpers.RunSync(() => CreateEmails(context));
            AsyncHelpers.RunSync(() => CreateRoles(context, roleManager));
            AsyncHelpers.RunSync(() => CreateTenants(context, userManager));
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
            if (!await context.Roles.AnyAsync(c => c.Name == ApplicationRoles.Tenant))
            {
                await roleManager.CreateAsync(new ApplicationRole(ApplicationRoles.Tenant));
            }
        }

        private static async Task CreateTenants(LLDbContext context, ApplicationUserManager userManager)
        {
            if (!await context.Users.AnyAsync(c => c.Email == "tenant@hotmail.co.uk"))
            {
                var user = new ApplicationUser
                {
                    UserName = "tenant@hotmail.co.uk",
                    Email = "tenant@hotmail.co.uk",
                    EmailConfirmed = true,
                    FirstName = "John",
                    MiddleName = "Paul",
                    LastName = "Tenant",
                    PhoneNumber = "07955567790",
                    SecondaryPhoneNumber = "01925555555",
                    Title = "Mr"
                };

                await userManager.CreateAsync(user, "Password123");
                await userManager.AddToRoleAsync(user, ApplicationRoles.Tenant);
                await userManager.SetUserPermissionsAsync(user.Id, DefaultPermissions.Tenant.Select(c => Guid.Parse(c)).ToArray());

                var tenant = new Tenant
                {
                    DateOfBirth = DateTime.Parse("1987-07-27 00:00:00"),
                    Created = DateTime.Now,
                    AdditionalInformation = "Additional",
                    DrivingLicenseReference = "Driving",
                    EmploymentType = EmploymentTypes.SelfEmployed,
                    HasPets = true,
                    IsAdult = true,
                    IsLeadTenant = true,
                    IsSmoker = true,
                    Occupation = "Web Developer",
                    PassportReference = "Passport",
                    CompanyName = "Jon Preece Development Ltd",
                    WorkAddress = "Work address",
                    WorkContactNumber = "Work Contact",
                    ApplicationUserId = user.Id
                };

                tenant.Addresses = new List<TenantAddress>
                {
                    new TenantAddress
                    {
                        Tenant = tenant,
                        Created = DateTime.Now,
                        Country = Countries.UnitedKingdom,
                        CountyOrRegion = Counties.Cheshire,
                        MonthsAtAddress = 6,
                        Postcode = "WA53SL",
                        Street = "Bridgeport Mews",
                        TownOrCity = "Warrington",
                        YearsAtAddress = 3
                    }
                };

                tenant.Contacts = new List<TenantContact>
                {
                    new TenantContact
                    {
                        Name = "Contact",
                        Tenant = tenant,
                        Comments = "Comments",
                        Created = DateTime.Now,
                        MainContactNumber = "07955555555",
                        Relationship = "Manager",
                        SecondaryContactNumber = "09111111"
                    }
                };

                await context.Tenants.AddAsync(tenant);
                await context.SaveChangesAsync();
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
                var administrator = new ApplicationUser
                {
                    UserName = "administrator@agency.co.uk",
                    Email = "administrator@agency.co.uk",
                    EmailConfirmed = true,
                    FirstName = "Agency",
                    LastName = "Administrator"
                };
                
                await userManager.CreateAsync(administrator, "Password123");
                await userManager.AddToRoleAsync(administrator, ApplicationRoles.AgencyAdministrator);
                await userManager.SetUserPermissionsAsync(administrator.Id, DefaultPermissions.AgencyAdministrator.Select(c => Guid.Parse(c)).ToArray());
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

                var agencyPortfolio = new Portfolio
                {
                    Created = DateTime.Now,
                    DisplayName = "Agency Portfolio",
                    Name = "Agency-BBB445"
                };
                await context.Portfolios.AddAsync(agencyPortfolio);
                await context.SaveChangesAsync();

                var link = new ApplicationUserPortfolio
                {
                    Created = DateTime.Now,
                    PortfolioId = agencyPortfolio.Id,
                    AgencyId = agency.Id,
                    UserId = user1.Id
                };
                await context.ApplicationUserPortfolios.AddAsync(link);
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
                        Order = 1,
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Take photos (or get a professional to do it)",
                        Key = "NL002",
                        Order = 2,
                        Template = ChecklistTemplates.DocumentUpload
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Advertise the property",
                        Key = "NL003",
                        Order = 3,
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Conduct viewings",
                        Key = "NL004",
                        Order = 4,
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Negotiate & accept offer",
                        Key = "NL005",
                        Order = 5,
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Take fees",
                        Key = "NL006",
                        Order = 6,
                        Template = ChecklistTemplates.CommentsOnly
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Get proof of Id",
                        Key = "NL007",
                        Order = 7,
                        Template = ChecklistTemplates.DocumentUpload
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Establish right to rent",
                        Key = "NL008",
                        Order = 8,
                        Template = ChecklistTemplates.CommentsAndDateOfAction
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Conduct reference checks",
                        Key = "NL009",
                        Order = 9,
                        Template = ChecklistTemplates.DateOfAction
                    },

                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveInChecklist.Id,
                        DisplayText = "Ask for a guarantor if necessary",
                        Key = "NL010",
                        Order = 10,
                        Template = ChecklistTemplates.CommentsOnly
                    }
                };

                await context.ChecklistItems.AddRangeAsync(checklistItems);
                await context.SaveChangesAsync();
            }

            if (!await context.Checklists.AnyAsync(c => c.Name == "Move the tenants out"))
            {
                var moveOutChecklist = new Checklist
                {
                    Created = DateTime.Now,
                    Name = "Move the tenants out",
                    Image = "checklist.png",
                    Description = "For when a new tenant is moving out of the property",
                    IsAvailableDownstream = true,
                    IsPropertyMandatory = false,
                    UserId = admin.Id
                };

                await context.Checklists.AddAsync(moveOutChecklist);
                await context.SaveChangesAsync();

                var checklistItems = new List<ChecklistItem>
                {
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Before move out, make sure the tenants are clear on their responsibilities and what is expected in order for them to get their deposit back in full",
                        IsExpanded = false,
                        Key = "MO001",
                        Order = 1,
                        Template = ChecklistTemplates.CommentsOnly
                    },
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Conduct a checkout inspection",
                        IsExpanded = false,
                        Key = "MO002",
                        Order = 2,
                        Template = ChecklistTemplates.DateOfAction
                    },
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Take meter readings",
                        IsExpanded = false,
                        Key = "MO003",
                        Order = 3,
                        Template = ChecklistTemplates.CommentsAndDateOfAction
                    },
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Contact the utility companies with the meter readings, the tenant's forwarding address, and the details of the new occupant",
                        IsExpanded = false,
                        Key = "MO004",
                        Order = 4,
                        Template = ChecklistTemplates.CommentsAndDateOfAction
                    },
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Agree on how much (if any) of the deposit should be deducted",
                        IsExpanded = false,
                        Key = "MO005",
                        Order = 5,
                        Template = ChecklistTemplates.CommentsOnly
                    },
                    new ChecklistItem
                    {
                        Created = DateTime.Now,
                        ChecklistId = moveOutChecklist.Id,
                        DisplayText = "Arrange the return of the deposit with the deposit protection scheme you've used",
                        IsExpanded = false,
                        Key = "MO006",
                        Order = 6,
                        Template = ChecklistTemplates.CommentsAndDateOfAction
                    }
                };

                await context.ChecklistItems.AddRangeAsync(checklistItems);
                await context.SaveChangesAsync();
            }

            if (!await context.Checklists.AnyAsync(c => c.Name == "Blank"))
            {
                var blankChecklist = new Checklist
                {
                    Created = DateTime.Now,
                    Name = "Blank",
                    Image = "checklist.png",
                    Description = "Blank checklist template",
                    IsAvailableDownstream = true,
                    IsPropertyMandatory = false,
                    UserId = admin.Id
                };

                await context.Checklists.AddAsync(blankChecklist);
                await context.SaveChangesAsync();
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