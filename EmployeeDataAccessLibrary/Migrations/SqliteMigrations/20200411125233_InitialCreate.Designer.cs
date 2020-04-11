﻿// <auto-generated />
using System;
using EmployeeDataAccessLibrary.DataAccess;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeDataAccessLibrary.Migrations.SqliteMigrations
{
    [DbContext(typeof(SqlitePeopleContext))]
    [Migration("20200411125233_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099");

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<int?>("PersonId");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<int?>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("EmailsAddresses");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Address", b =>
                {
                    b.HasOne("EmployeeDataAccessLibrary.Models.Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Email", b =>
                {
                    b.HasOne("EmployeeDataAccessLibrary.Models.Person")
                        .WithMany("EmailAddresses")
                        .HasForeignKey("PersonId");
                });
#pragma warning restore 612, 618
        }
    }
}