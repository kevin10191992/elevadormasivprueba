﻿// <auto-generated />
using System;
using Elevador.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elevador.Migrations
{
    [DbContext(typeof(ElevatorContext))]
    [Migration("20220227140706_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Elevador.Models.ElevatorFloor", b =>
                {
                    b.Property<int>("ElevatorFloorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElevatorFloorId"), 1L, 1);

                    b.Property<int>("CurrentElevatorFloor")
                        .HasColumnType("int");

                    b.Property<DateTime>("CurrenteTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ElevatorFloorId");

                    b.ToTable("ElevatorFloor");
                });

            modelBuilder.Entity("Elevador.Models.ElevatorWork", b =>
                {
                    b.Property<int>("ElevatorWorkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ElevatorWorkId"), 1L, 1);

                    b.Property<bool>("CalledFromInside")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CompletedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FromFloor")
                        .HasColumnType("int");

                    b.Property<bool>("RequestCompleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ToFloor")
                        .HasColumnType("int");

                    b.HasKey("ElevatorWorkId");

                    b.ToTable("ElevatorWork");
                });
#pragma warning restore 612, 618
        }
    }
}
