using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ControleAcesso_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PERMISSAO_USUARIO",
                table: "PERMISSAO_USUARIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERMISSAO_USUARIO",
                table: "PERMISSAO_USUARIO",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSAO_USUARIO_UsuarioId",
                table: "PERMISSAO_USUARIO",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PERMISSAO_USUARIO",
                table: "PERMISSAO_USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_PERMISSAO_USUARIO_UsuarioId",
                table: "PERMISSAO_USUARIO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PERMISSAO_USUARIO",
                table: "PERMISSAO_USUARIO",
                columns: new[] { "UsuarioId", "PermissaoId" });
        }
    }
}
