﻿// <auto-generated />
using System;
using EmployeeDataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeDataAccessLibrary.Migrations
{
    [DbContext(typeof(PeopleContext))]
    [Migration("20200219164157_AddAgeToPerson")]
    partial class AddAgeToPerson
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<int?>("PersonId");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("EmailAddresses");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress");

                    b.Property<int?>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("EmailsAddresses");
                });

            modelBuilder.Entity("EmployeeDataAccessLibrary.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
