using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AddNovosCamposEmItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "lucro_item",
                table: "itens_pedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "preco_total",
                table: "itens_pedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "preco_unitario",
                table: "itens_pedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lucro_item",
                table: "itens_pedido");

            migrationBuilder.DropColumn(
                name: "preco_total",
                table: "itens_pedido");

            migrationBuilder.DropColumn(
                name: "preco_unitario",
                table: "itens_pedido");
        }
    }
}
