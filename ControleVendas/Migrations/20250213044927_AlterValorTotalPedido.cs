using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterValorTotalPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "valor_total",
                table: "pedidos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "valor_total",
                table: "pedidos");
        }
    }
}
