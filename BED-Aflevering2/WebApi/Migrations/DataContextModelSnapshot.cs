﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Data;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobModel", b =>
                {
                    b.Property<long>("JobsJobId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModelsModelId")
                        .HasColumnType("bigint");

                    b.HasKey("JobsJobId", "ModelsModelId");

                    b.HasIndex("ModelsModelId");

                    b.ToTable("JobModel");
                });

            modelBuilder.Entity("WebApi.Models.Expense", b =>
                {
                    b.Property<long>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ExpenseId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<long>("JobId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModelId")
                        .HasColumnType("bigint");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("ExpenseId");

                    b.HasIndex("JobId");

                    b.HasIndex("ModelId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("WebApi.Models.Job", b =>
                {
                    b.Property<long>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("JobId"));

                    b.Property<string>("Comments")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Customer")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("JobId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("WebApi.Models.Model", b =>
                {
                    b.Property<long>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ModelId"));

                    b.Property<string>("AddresLine1")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("AddresLine2")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Comments")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Email")
                        .HasMaxLength(254)
                        .HasColumnType("nvarchar(254)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("HairColor")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("LastName")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("ShoeSize")
                        .HasColumnType("int");

                    b.Property<string>("Zip")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.HasKey("ModelId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("JobModel", b =>
                {
                    b.HasOne("WebApi.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Model", null)
                        .WithMany()
                        .HasForeignKey("ModelsModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Expense", b =>
                {
                    b.HasOne("WebApi.Models.Job", null)
                        .WithMany("Expenses")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Models.Model", null)
                        .WithMany("Expenses")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.Models.Job", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("WebApi.Models.Model", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
