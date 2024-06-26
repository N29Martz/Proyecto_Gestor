﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using gestor_archivos_backend.DataBase;

#nullable disable

namespace gestor_archivos_backend.Migrations
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20240503212647_DbLogs")]
    partial class DbLogs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("gestor_archivos_backend.Entities.Logs.LogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateTime")
                        .HasMaxLength(100)
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("description");

                    b.Property<Guid?>("FileId")
                        .HasColumnType("RAW(16)")
                        .HasColumnName("file_id");

                    b.Property<Guid?>("LogTypeId")
                        .IsRequired()
                        .HasColumnType("RAW(16)")
                        .HasColumnName("log_type_id");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("LogTypeId");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("gestor_archivos_backend.Entities.Logs.LogTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("RAW(16)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("Id");

                    b.ToTable("LogTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("57f82d3b-dbbd-48ae-b897-20b0a7d8e133"),
                            Description = "Inicio de sesión"
                        },
                        new
                        {
                            Id = new Guid("d3186c86-17b2-434c-bd10-2883be208460"),
                            Description = "Cierre de sesión"
                        },
                        new
                        {
                            Id = new Guid("11f16c93-c939-4fd6-8d0a-da46dee1e5d7"),
                            Description = "Registro de usuario"
                        },
                        new
                        {
                            Id = new Guid("1aa82931-f9da-47c7-8f92-3209b4af0902"),
                            Description = "Modificación de usuario"
                        },
                        new
                        {
                            Id = new Guid("15239952-1e3f-4665-a897-d8c5bbbd8cfb"),
                            Description = "Eliminación de usuario"
                        },
                        new
                        {
                            Id = new Guid("4abd620e-7f48-4b0f-aaea-223c0376215b"),
                            Description = "Creación de archivo"
                        },
                        new
                        {
                            Id = new Guid("010da127-7422-4cb6-87d2-15444479140b"),
                            Description = "Lectura de archivos"
                        },
                        new
                        {
                            Id = new Guid("cb630c49-3ee4-47fb-9cf9-30199a5f5bf7"),
                            Description = "Eliminación de archivo"
                        },
                        new
                        {
                            Id = new Guid("c854d80a-0aff-4d8b-827b-5d44e0e5e85c"),
                            Description = "Modificación de archivo"
                        },
                        new
                        {
                            Id = new Guid("8358d9ff-888b-4bc4-884a-1ee5d1d8a6fe"),
                            Description = "Creación de carpeta"
                        },
                        new
                        {
                            Id = new Guid("edfd838e-4766-44d8-b5c4-d3e971f906fe"),
                            Description = "Lectura de carpeta"
                        },
                        new
                        {
                            Id = new Guid("2ea8aac7-aa64-4f0f-a43f-813d99d44f9f"),
                            Description = "Eliminación de carpeta"
                        },
                        new
                        {
                            Id = new Guid("fc60b86c-a37f-493f-8d4e-1684bcb373dd"),
                            Description = "Modificación de carpeta"
                        });
                });

            modelBuilder.Entity("gestor_archivos_backend.Entities.Logs.LogEntity", b =>
                {
                    b.HasOne("gestor_archivos_backend.Entities.Logs.LogTypeEntity", "LogType")
                        .WithMany()
                        .HasForeignKey("LogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogType");
                });
#pragma warning restore 612, 618
        }
    }
}
