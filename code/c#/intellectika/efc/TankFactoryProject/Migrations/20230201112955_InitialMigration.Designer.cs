﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MainEntityProject.Model.Context;

#nullable disable

namespace MainEntityProject.Migrations
{
    [DbContext(typeof(VehicleDatabaseContext))]
    [Migration("20230201112955_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TankFactory.Model.Entities.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Engine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HorsePowers")
                        .HasColumnType("int");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("PriceId");

                    b.ToTable("Engines");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Gun", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaliberMillimetres")
                        .HasColumnType("int");

                    b.Property<int>("LengthInCalibers")
                        .HasColumnType("int");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("PriceId");

                    b.ToTable("Guns");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.MainBattleTank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CrewCount")
                        .HasColumnType("int");

                    b.Property<int>("EngineId")
                        .HasColumnType("int");

                    b.Property<int>("GunId")
                        .HasColumnType("int");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineId");

                    b.HasIndex("GunId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("PriceId");

                    b.ToTable("MainBattleTank");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BudgetId")
                        .HasColumnType("int");

                    b.Property<int?>("BudgetId1")
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId")
                        .IsUnique();

                    b.HasIndex("BudgetId1");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Value")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Engine", b =>
                {
                    b.HasOne("TankFactory.Model.Entities.Manufacturer", "ManufacturerReference")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Price", "PriceReference")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManufacturerReference");

                    b.Navigation("PriceReference");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Gun", b =>
                {
                    b.HasOne("TankFactory.Model.Entities.Manufacturer", "ManufacturerReference")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Price", "PriceReference")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManufacturerReference");

                    b.Navigation("PriceReference");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.MainBattleTank", b =>
                {
                    b.HasOne("TankFactory.Model.Entities.Engine", "EngineReference")
                        .WithMany()
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Gun", "GunReference")
                        .WithMany()
                        .HasForeignKey("GunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Manufacturer", "ManufacturerReference")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Price", "PriceReference")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineReference");

                    b.Navigation("GunReference");

                    b.Navigation("ManufacturerReference");

                    b.Navigation("PriceReference");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Manufacturer", b =>
                {
                    b.HasOne("TankFactory.Model.Entities.Budget", "BudgetReference")
                        .WithOne("ManufacturerReference")
                        .HasForeignKey("TankFactory.Model.Entities.Manufacturer", "BudgetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TankFactory.Model.Entities.Budget", null)
                        .WithMany()
                        .HasForeignKey("BudgetId1");

                    b.Navigation("BudgetReference");
                });

            modelBuilder.Entity("TankFactory.Model.Entities.Budget", b =>
                {
                    b.Navigation("ManufacturerReference");
                });
#pragma warning restore 612, 618
        }
    }
}
