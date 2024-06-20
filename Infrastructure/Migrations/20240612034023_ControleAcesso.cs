using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ControleAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERMISSAO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COD_PERMISSAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_DESCRICAO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSAO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COD_USUARIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NOM_USUARIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_SENHA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TXT_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IND_EMAIL_CONFIRMADO = table.Column<bool>(type: "bit", nullable: false),
                    IND_ATIVO = table.Column<bool>(type: "bit", nullable: false),
                    DT_REGISTRO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DT_ALTERACAO = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSAO_USUARIO",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSAO_USUARIO", x => new { x.UsuarioId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_PERMISSAO_USUARIO_PERMISSAO_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "PERMISSAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERMISSAO_USUARIO_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSAO_USUARIO_PermissaoId",
                table: "PERMISSAO_USUARIO",
                column: "PermissaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PERMISSAO_USUARIO");

            migrationBuilder.DropTable(
                name: "PERMISSAO");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
