using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableColumnPedidoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoId",
                table: "itens_pedidos");

            migrationBuilder.DropIndex(
                name: "IX_itens_pedidos_PedidoId",
                table: "itens_pedidos");

            migrationBuilder.RenameColumn(
                name: "PedidoId",
                table: "itens_pedidos",
                newName: "pedido_id");

            migrationBuilder.AlterColumn<int>(
                name: "pedido_id",
                table: "itens_pedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "pedido_id",
                table: "itens_pedidos",
                newName: "PedidoId");

            migrationBuilder.AlterColumn<int>(
                name: "PedidoId",
                table: "itens_pedidos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_itens_pedidos_PedidoId",
                table: "itens_pedidos",
                column: "PedidoId");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoId",
                table: "itens_pedidos",
                column: "PedidoId",
                principalTable: "pedidos",
                principalColumn: "id");
        }
    }
}
