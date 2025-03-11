using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDataVendaTablePedido1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE pedidos 
            ALTER COLUMN data_venda SET DEFAULT (NOW() AT TIME ZONE 'America/Sao_Paulo');
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE pedidos 
            ALTER COLUMN data_venda SET DEFAULT (NOW() AT TIME ZONE 'UTC');
        ");
        }
    }
}
