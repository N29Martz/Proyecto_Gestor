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
    [Migration("20240503225300_ChangeLimitDescriptionLog")]
    partial class ChangeLimitDescriptionLog
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
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
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
                            Id = new Guid("3bf0fd54-6a0a-4434-9ee4-ba53f76a16a1"),
                            Description = "Inicio de sesión"
                        },
                        new
                        {
                            Id = new Guid("6ddab7ff-2cf8-48c3-bd7c-92ce1a6015bb"),
                            Description = "Cierre de sesión"
                        },
                        new
                        {
                            Id = new Guid("659631fc-70db-4962-b9b0-5ebb403d2b8e"),
                            Description = "Registro de usuario"
                        },
                        new
                        {
                            Id = new Guid("240fe589-283a-4c0e-b2ad-4e398832da4f"),
                            Description = "Modificación de usuario"
                        },
                        new
                        {
                            Id = new Guid("8453e4ec-275f-477c-9c1a-1aa298fce74a"),
                            Description = "Eliminación de usuario"
                        },
                        new
                        {
                            Id = new Guid("cadc8bd3-5f57-4a33-b1a5-8d714b5e0d4c"),
                            Description = "Creación de archivo"
                        },
                        new
                        {
                            Id = new Guid("ce1c572b-e4c4-4347-b1f0-1f57d6105b39"),
                            Description = "Lectura de archivos"
                        },
                        new
                        {
                            Id = new Guid("af054cc4-b97a-433b-88f6-258e7db701e3"),
                            Description = "Eliminación de archivo"
                        },
                        new
                        {
                            Id = new Guid("40f9d5f6-3c3d-4d98-8e9c-aa45fe861329"),
                            Description = "Modificación de archivo"
                        },
                        new
                        {
                            Id = new Guid("0ee4d072-67f9-4a4a-ba33-d4708e85787a"),
                            Description = "Creación de carpeta"
                        },
                        new
                        {
                            Id = new Guid("a12d6ab8-f2ec-4135-a65b-f24fdb3911c1"),
                            Description = "Lectura de carpeta"
                        },
                        new
                        {
                            Id = new Guid("c0803d8c-76f1-4b9f-9ce5-be0657942b76"),
                            Description = "Eliminación de carpeta"
                        },
                        new
                        {
                            Id = new Guid("f2a1521c-d508-4f58-ad3b-ac83dfa98a47"),
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
