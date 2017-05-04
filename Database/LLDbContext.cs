﻿namespace Landlords.Database
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Model;
    using Model.Database;

    public class LLDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, ILLDbContext
    {
        public LLDbContext()
        {
            
        }

        public LLDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PropertyDetails>()
                .HasOne(p => p.User)
                .WithMany()
                .HasConstraintName("ForeignKey_User_PropertyDetails");
            
            builder.Entity<PropertyImage>()
                .HasOne(p => p.User)
                .WithMany()
                .HasConstraintName("ForeignKey_User_PropertyImages");

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<PropertyDetails> PropertyDetails { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
    }
}