using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoDescricaoTamanhoAlterado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "produtos",
                type: "character varying(280)",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "produtos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(280)",
                oldMaxLength: 280,
                oldNullable: true);
        }
    }
}
