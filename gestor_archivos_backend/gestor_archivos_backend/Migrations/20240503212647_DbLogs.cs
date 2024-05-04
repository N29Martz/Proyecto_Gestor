using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gestor_archivos_backend.Migrations
{
    /// <inheritdoc />
    public partial class DbLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    description = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    datetime = table.Column<DateTime>(type: "TIMESTAMP(7)", maxLength: 100, nullable: false),
                    file_id = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    log_type_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_LogTypes_log_type_id",
                        column: x => x.log_type_id,
                        principalTable: "LogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LogTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("010da127-7422-4cb6-87d2-15444479140b"), "Lectura de archivos" },
                    { new Guid("11f16c93-c939-4fd6-8d0a-da46dee1e5d7"), "Registro de usuario" },
                    { new Guid("15239952-1e3f-4665-a897-d8c5bbbd8cfb"), "Eliminación de usuario" },
                    { new Guid("1aa82931-f9da-47c7-8f92-3209b4af0902"), "Modificación de usuario" },
                    { new Guid("2ea8aac7-aa64-4f0f-a43f-813d99d44f9f"), "Eliminación de carpeta" },
                    { new Guid("4abd620e-7f48-4b0f-aaea-223c0376215b"), "Creación de archivo" },
                    { new Guid("57f82d3b-dbbd-48ae-b897-20b0a7d8e133"), "Inicio de sesión" },
                    { new Guid("8358d9ff-888b-4bc4-884a-1ee5d1d8a6fe"), "Creación de carpeta" },
                    { new Guid("c854d80a-0aff-4d8b-827b-5d44e0e5e85c"), "Modificación de archivo" },
                    { new Guid("cb630c49-3ee4-47fb-9cf9-30199a5f5bf7"), "Eliminación de archivo" },
                    { new Guid("d3186c86-17b2-434c-bd10-2883be208460"), "Cierre de sesión" },
                    { new Guid("edfd838e-4766-44d8-b5c4-d3e971f906fe"), "Lectura de carpeta" },
                    { new Guid("fc60b86c-a37f-493f-8d4e-1684bcb373dd"), "Modificación de carpeta" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_log_type_id",
                table: "logs",
                column: "log_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "LogTypes");
        }
    }
}
