using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDataCadastroProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE produtos 
            ALTER COLUMN data_cadastro SET DEFAULT (NOW() AT TIME ZONE 'America/Sao_Paulo');
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE produtos 
            ALTER COLUMN data_cadastro SET DEFAULT (NOW() AT TIME ZONE 'UTC');
        ");
        }
    }
}
