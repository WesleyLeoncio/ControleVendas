using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableAddForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoEntityId",
                table: "itens_pedidos");

            migrationBuilder.DropIndex(
                name: "IX_itens_pedidos_PedidoEntityId",
                table: "itens_pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoEntityId",
                table: "itens_pedidos");

            migrationBuilder.CreateIndex(
                name: "IX_itens_pedidos_pedido_id",
                table: "itens_pedidos",
                column: "pedido_id");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedidos_pedidos_pedido_id",
                table: "itens_pedidos",
                column: "pedido_id",
                principalTable: "pedidos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedidos_pedidos_pedido_id",
                table: "itens_pedidos");

            migrationBuilder.DropIndex(
                name: "IX_itens_pedidos_pedido_id",
                table: "itens_pedidos");

            migrationBuilder.AddColumn<int>(
                name: "PedidoEntityId",
                table: "itens_pedidos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_itens_pedidos_PedidoEntityId",
                table: "itens_pedidos",
                column: "PedidoEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoEntityId",
                table: "itens_pedidos",
                column: "PedidoEntityId",
                principalTable: "pedidos",
                principalColumn: "id");
        }
    }
}
