using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValueToDataCadastro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adiciona o valor padrão CURRENT_TIMESTAMP à coluna data_cadastro
            migrationBuilder.AlterColumn<DateTime>(
                name: "data_cadastro",
                table: "produtos",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverte a alteração, removendo o valor padrão
            migrationBuilder.AlterColumn<DateTime>(
                name: "data_cadastro",
                table: "produtos",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
