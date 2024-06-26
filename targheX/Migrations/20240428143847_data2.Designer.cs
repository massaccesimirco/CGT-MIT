﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using targheX.Data;

#nullable disable

namespace targheX.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240428143847_data2")]
    partial class data2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("targheX.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AgostoCarico")
                        .HasColumnType("int");

                    b.Property<int>("AgostoScarico")
                        .HasColumnType("int");

                    b.Property<int>("AprileCarico")
                        .HasColumnType("int");

                    b.Property<int>("AprileScarico")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataIns")
                        .HasColumnType("datetime2");

                    b.Property<int>("DicembreCarico")
                        .HasColumnType("int");

                    b.Property<int>("DicembreScarico")
                        .HasColumnType("int");

                    b.Property<int>("FebbraioCarico")
                        .HasColumnType("int");

                    b.Property<int>("FebbraioScarico")
                        .HasColumnType("int");

                    b.Property<int>("GennaioCarico")
                        .HasColumnType("int");

                    b.Property<int>("GennaioScarico")
                        .HasColumnType("int");

                    b.Property<int>("Giacenza")
                        .HasColumnType("int");

                    b.Property<int>("GiugnoCarico")
                        .HasColumnType("int");

                    b.Property<int>("GiugnoScarico")
                        .HasColumnType("int");

                    b.Property<int>("LuglioCarico")
                        .HasColumnType("int");

                    b.Property<int>("LuglioScarico")
                        .HasColumnType("int");

                    b.Property<int>("MaggioCarico")
                        .HasColumnType("int");

                    b.Property<int>("MaggioScarico")
                        .HasColumnType("int");

                    b.Property<int>("MarzoCarico")
                        .HasColumnType("int");

                    b.Property<int>("MarzoScarico")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NovembreCarico")
                        .HasColumnType("int");

                    b.Property<int>("NovembreScarico")
                        .HasColumnType("int");

                    b.Property<int>("NuovoValore")
                        .HasColumnType("int");

                    b.Property<int>("OttobreCarico")
                        .HasColumnType("int");

                    b.Property<int>("OttobreScarico")
                        .HasColumnType("int");

                    b.Property<int>("Rimanenza")
                        .HasColumnType("int");

                    b.Property<int>("SettembreCarico")
                        .HasColumnType("int");

                    b.Property<int>("SettembreScarico")
                        .HasColumnType("int");

                    b.Property<int>("Totale")
                        .HasColumnType("int");

                    b.Property<int>("TotaleCarico")
                        .HasColumnType("int");

                    b.Property<int>("TotaleScarico")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
