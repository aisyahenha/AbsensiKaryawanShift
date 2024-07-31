﻿// <auto-generated />
using System;
using Absensi.Microservice.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Absensi.Microservice.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240730071500_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Absensi.Microservice.Models.AbsensiModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateIn")
                        .HasColumnType("date");

                    b.Property<int>("KaryawanId")
                        .HasColumnType("int");

                    b.Property<string>("StatusKeluar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusMasuk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("TimeIn")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TimeOut")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("KaryawanId");

                    b.ToTable("m_absensi");
                });

            modelBuilder.Entity("Absensi.Microservice.Models.KaryawanModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CratedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Shift")
                        .HasColumnType("bit");

                    b.Property<string>("ShiftDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NIK")
                        .IsUnique();

                    b.ToTable("m_karyawan");
                });

            modelBuilder.Entity("Absensi.Microservice.Models.AbsensiModel", b =>
                {
                    b.HasOne("Absensi.Microservice.Models.KaryawanModel", "Karyawan")
                        .WithMany()
                        .HasForeignKey("KaryawanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Karyawan");
                });
#pragma warning restore 612, 618
        }
    }
}
