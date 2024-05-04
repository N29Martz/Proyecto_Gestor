using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gestor_archivos_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLimitDescriptionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("010da127-7422-4cb6-87d2-15444479140b"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("11f16c93-c939-4fd6-8d0a-da46dee1e5d7"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("15239952-1e3f-4665-a897-d8c5bbbd8cfb"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("1aa82931-f9da-47c7-8f92-3209b4af0902"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("2ea8aac7-aa64-4f0f-a43f-813d99d44f9f"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("4abd620e-7f48-4b0f-aaea-223c0376215b"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("57f82d3b-dbbd-48ae-b897-20b0a7d8e133"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("8358d9ff-888b-4bc4-884a-1ee5d1d8a6fe"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("c854d80a-0aff-4d8b-827b-5d44e0e5e85c"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("cb630c49-3ee4-47fb-9cf9-30199a5f5bf7"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("d3186c86-17b2-434c-bd10-2883be208460"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("edfd838e-4766-44d8-b5c4-d3e971f906fe"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("fc60b86c-a37f-493f-8d4e-1684bcb373dd"));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "logs",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                table: "LogTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { new Guid("0ee4d072-67f9-4a4a-ba33-d4708e85787a"), "Creación de carpeta" },
                    { new Guid("240fe589-283a-4c0e-b2ad-4e398832da4f"), "Modificación de usuario" },
                    { new Guid("3bf0fd54-6a0a-4434-9ee4-ba53f76a16a1"), "Inicio de sesión" },
                    { new Guid("40f9d5f6-3c3d-4d98-8e9c-aa45fe861329"), "Modificación de archivo" },
                    { new Guid("659631fc-70db-4962-b9b0-5ebb403d2b8e"), "Registro de usuario" },
                    { new Guid("6ddab7ff-2cf8-48c3-bd7c-92ce1a6015bb"), "Cierre de sesión" },
                    { new Guid("8453e4ec-275f-477c-9c1a-1aa298fce74a"), "Eliminación de usuario" },
                    { new Guid("a12d6ab8-f2ec-4135-a65b-f24fdb3911c1"), "Lectura de carpeta" },
                    { new Guid("af054cc4-b97a-433b-88f6-258e7db701e3"), "Eliminación de archivo" },
                    { new Guid("c0803d8c-76f1-4b9f-9ce5-be0657942b76"), "Eliminación de carpeta" },
                    { new Guid("cadc8bd3-5f57-4a33-b1a5-8d714b5e0d4c"), "Creación de archivo" },
                    { new Guid("ce1c572b-e4c4-4347-b1f0-1f57d6105b39"), "Lectura de archivos" },
                    { new Guid("f2a1521c-d508-4f58-ad3b-ac83dfa98a47"), "Modificación de carpeta" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("0ee4d072-67f9-4a4a-ba33-d4708e85787a"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("240fe589-283a-4c0e-b2ad-4e398832da4f"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("3bf0fd54-6a0a-4434-9ee4-ba53f76a16a1"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("40f9d5f6-3c3d-4d98-8e9c-aa45fe861329"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("659631fc-70db-4962-b9b0-5ebb403d2b8e"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ddab7ff-2cf8-48c3-bd7c-92ce1a6015bb"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("8453e4ec-275f-477c-9c1a-1aa298fce74a"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("a12d6ab8-f2ec-4135-a65b-f24fdb3911c1"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("af054cc4-b97a-433b-88f6-258e7db701e3"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("c0803d8c-76f1-4b9f-9ce5-be0657942b76"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("cadc8bd3-5f57-4a33-b1a5-8d714b5e0d4c"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("ce1c572b-e4c4-4347-b1f0-1f57d6105b39"));

            migrationBuilder.DeleteData(
                table: "LogTypes",
                keyColumn: "Id",
                keyValue: new Guid("f2a1521c-d508-4f58-ad3b-ac83dfa98a47"));

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "logs",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

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
        }
    }
}
