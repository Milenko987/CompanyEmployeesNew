﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace CompanyEmployeesNew.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20230120173012_initial-data")]
    partial class initialdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"),
                            Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                            Country = "USA",
                            Name = "IT_Solutions Ltd"
                        },
                        new
                        {
                            Id = new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9"),
                            Address = "312 Forest Avenue, BF 923",
                            Country = "USA",
                            Name = "Admin_Solutions Ltd"
                        });
                });

            modelBuilder.Entity("Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EmployeeId");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b70a7067-a271-41c8-b996-2722a3410278"),
                            Age = 26,
                            CompanyId = new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"),
                            Name = "Sam Raiden",
                            Position = "Software developer"
                        },
                        new
                        {
                            Id = new Guid("c9977417-2d8d-4b00-8c7a-134d8dcecf8f"),
                            Age = 30,
                            CompanyId = new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"),
                            Name = "Jana McLeaf",
                            Position = "Software developer"
                        },
                        new
                        {
                            Id = new Guid("8b831a47-e957-4beb-ba2b-18b5a1ffc610"),
                            Age = 35,
                            CompanyId = new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9"),
                            Name = "Kane Miller",
                            Position = "Administrator"
                        });
                });

            modelBuilder.Entity("Entities.Employee", b =>
                {
                    b.HasOne("Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Entities.Company", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
