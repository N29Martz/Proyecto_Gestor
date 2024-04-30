using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gestor_archivos_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permisos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    descripcion = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permisos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    descripcion = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_archivos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    descripcion = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_archivos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    nombre = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    apellido = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    fecha_registro = table.Column<string>(type: "NVARCHAR2(10)", nullable: false),
                    UserName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RoleId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ClaimValue = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_claims_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carpetas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    nombre = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    usuario_id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    carpeta_padre_id = table.Column<Guid>(type: "RAW(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpetas", x => x.id);
                    table.ForeignKey(
                        name: "FK_carpetas_carpetas_carpeta_padre_id",
                        column: x => x.carpeta_padre_id,
                        principalTable: "carpetas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_carpetas_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ClaimValue = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_claims_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_user_logins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    RoleId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Value = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_user_tokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "archivos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    nombre = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    tamanio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    tipo_archivo_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    carpeta_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    url_archivo = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos", x => x.id);
                    table.ForeignKey(
                        name: "FK_archivos_carpetas_carpeta_id",
                        column: x => x.carpeta_id,
                        principalTable: "carpetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_tipos_archivos_tipo_archivo_id",
                        column: x => x.tipo_archivo_id,
                        principalTable: "tipos_archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carpetas_compartidas",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    usuario_id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    carpeta_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    permiso_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpetas_compartidas", x => x.id);
                    table.ForeignKey(
                        name: "FK_carpetas_compartidas_carpetas_carpeta_id",
                        column: x => x.carpeta_id,
                        principalTable: "carpetas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carpetas_compartidas_permisos_permiso_id",
                        column: x => x.permiso_id,
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carpetas_compartidas_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "archivos_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    usuario_id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    archivo_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    permiso_id = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivos_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_archivos_usuarios_archivos_archivo_id",
                        column: x => x.archivo_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_usuarios_permisos_permiso_id",
                        column: x => x.permiso_id,
                        principalTable: "permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_archivos_usuarios_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "versiones_archivo",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    usuario_id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    archivo_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    descripcion = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    fecha_edicion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_versiones_archivo", x => x.id);
                    table.ForeignKey(
                        name: "FK_versiones_archivo_archivos_archivo_id",
                        column: x => x.archivo_id,
                        principalTable: "archivos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_versiones_archivo_users_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_archivos_carpeta_id",
                table: "archivos",
                column: "carpeta_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_tipo_archivo_id",
                table: "archivos",
                column: "tipo_archivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_usuarios_archivo_id",
                table: "archivos_usuarios",
                column: "archivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_usuarios_permiso_id",
                table: "archivos_usuarios",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "IX_archivos_usuarios_usuario_id",
                table: "archivos_usuarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_carpeta_padre_id",
                table: "carpetas",
                column: "carpeta_padre_id");

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_usuario_id",
                table: "carpetas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_compartidas_carpeta_id",
                table: "carpetas_compartidas",
                column: "carpeta_id");

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_compartidas_permiso_id",
                table: "carpetas_compartidas",
                column: "permiso_id");

            migrationBuilder.CreateIndex(
                name: "IX_carpetas_compartidas_usuario_id",
                table: "carpetas_compartidas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_claims_RoleId",
                table: "role_claims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "NormalizedName",
                unique: true,
                filter: "\"NormalizedName\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_user_claims_UserId",
                table: "user_claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_logins_UserId",
                table: "user_logins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true,
                filter: "\"NormalizedUserName\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_versiones_archivo_archivo_id",
                table: "versiones_archivo",
                column: "archivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_versiones_archivo_usuario_id",
                table: "versiones_archivo",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivos_usuarios");

            migrationBuilder.DropTable(
                name: "carpetas_compartidas");

            migrationBuilder.DropTable(
                name: "role_claims");

            migrationBuilder.DropTable(
                name: "user_claims");

            migrationBuilder.DropTable(
                name: "user_logins");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "user_tokens");

            migrationBuilder.DropTable(
                name: "versiones_archivo");

            migrationBuilder.DropTable(
                name: "permisos");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "archivos");

            migrationBuilder.DropTable(
                name: "carpetas");

            migrationBuilder.DropTable(
                name: "tipos_archivos");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
