using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Landlords.Data;
using Model;

namespace Landlords.Migrations
{
    [DbContext(typeof(LLContext))]
    partial class LLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Model.PropertyOverview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ConstructionDate");

                    b.Property<int>("Furnishing");

                    b.Property<decimal>("TargetRent");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("PropertyOverview");
                });
        }
    }
}
