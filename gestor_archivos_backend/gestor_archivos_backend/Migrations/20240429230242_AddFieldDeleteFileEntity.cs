using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gestor_archivos_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldDeleteFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("2a292041-6802-4fbc-8a39-70121011f9c1"));

            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("bd144632-e8f8-4eea-b526-38ec2cd139db"));

            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("bd661d24-0a8d-4d58-b880-5fabdb776ff3"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("22813103-832d-414d-81ea-0ed2d618725c"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("75d6a169-99d3-466f-9e7b-55a2c58b62c3"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("8ca6b399-5994-42fd-b44a-f156c77fa5b0"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("b8c55dfe-3fa5-490d-a71b-f0751f918e35"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("ba5799c7-5902-4dc9-bd98-9b065082f7ad"));

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "archivos",
                type: "NUMBER(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { new Guid("186b9671-91f2-4fea-93ee-4d6068e3fb00"), "Administrador" },
                    { new Guid("eccdd6e1-2fc7-4d65-93cd-da64ca3db0ae"), "Lectura y Escritura" },
                    { new Guid("fe75dbcb-fa77-487a-9a97-5ea585e12b62"), "Lectura" }
                });

            migrationBuilder.InsertData(
                table: "tipos_archivos",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { new Guid("73f9d6cb-b937-49e0-8165-e3c96e70e1a8"), "Audio" },
                    { new Guid("b7786780-4192-42ad-b7fb-acba92991af4"), "Otro" },
                    { new Guid("cf300a7f-73f8-455c-b596-4a589af875d1"), "Imagen" },
                    { new Guid("f0710718-03a9-46dd-834b-d962128797c2"), "Video" },
                    { new Guid("f41c2871-3993-4402-8e40-882acf5e6387"), "Documento" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("186b9671-91f2-4fea-93ee-4d6068e3fb00"));

            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("eccdd6e1-2fc7-4d65-93cd-da64ca3db0ae"));

            migrationBuilder.DeleteData(
                table: "permisos",
                keyColumn: "id",
                keyValue: new Guid("fe75dbcb-fa77-487a-9a97-5ea585e12b62"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("73f9d6cb-b937-49e0-8165-e3c96e70e1a8"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("b7786780-4192-42ad-b7fb-acba92991af4"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("cf300a7f-73f8-455c-b596-4a589af875d1"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("f0710718-03a9-46dd-834b-d962128797c2"));

            migrationBuilder.DeleteData(
                table: "tipos_archivos",
                keyColumn: "id",
                keyValue: new Guid("f41c2871-3993-4402-8e40-882acf5e6387"));

            migrationBuilder.DropColumn(
                name: "deleted",
                table: "archivos");

            migrationBuilder.InsertData(
                table: "permisos",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { new Guid("2a292041-6802-4fbc-8a39-70121011f9c1"), "Lectura" },
                    { new Guid("bd144632-e8f8-4eea-b526-38ec2cd139db"), "Administrador" },
                    { new Guid("bd661d24-0a8d-4d58-b880-5fabdb776ff3"), "Lectura y Escritura" }
                });

            migrationBuilder.InsertData(
                table: "tipos_archivos",
                columns: new[] { "id", "descripcion" },
                values: new object[,]
                {
                    { new Guid("22813103-832d-414d-81ea-0ed2d618725c"), "Otro" },
                    { new Guid("75d6a169-99d3-466f-9e7b-55a2c58b62c3"), "Audio" },
                    { new Guid("8ca6b399-5994-42fd-b44a-f156c77fa5b0"), "Imagen" },
                    { new Guid("b8c55dfe-3fa5-490d-a71b-f0751f918e35"), "Video" },
                    { new Guid("ba5799c7-5902-4dc9-bd98-9b065082f7ad"), "Documento" }
                });
        }
    }
}
