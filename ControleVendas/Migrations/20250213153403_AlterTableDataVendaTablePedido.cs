using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDataVendaTablePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE pedidos 
            ALTER COLUMN data_venda TYPE TIMESTAMPTZ USING data_venda AT TIME ZONE 'America/Sao_Paulo';
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE pedidos 
            ALTER COLUMN data_venda TYPE TIMESTAMP WITHOUT TIME ZONE USING data_venda;
        ");
        }
    }
}
