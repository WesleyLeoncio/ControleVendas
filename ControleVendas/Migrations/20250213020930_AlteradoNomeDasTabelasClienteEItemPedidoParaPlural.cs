using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoNomeDasTabelasClienteEItemPedidoParaPlural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedido_pedidos_PedidoId",
                table: "itens_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedido_produtos_produto_id",
                table: "itens_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_cliente_cliente_id",
                table: "pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itens_pedido",
                table: "itens_pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cliente",
                table: "cliente");

            migrationBuilder.RenameTable(
                name: "itens_pedido",
                newName: "itens_pedidos");

            migrationBuilder.RenameTable(
                name: "cliente",
                newName: "clientes");

            migrationBuilder.RenameIndex(
                name: "IX_itens_pedido_produto_id",
                table: "itens_pedidos",
                newName: "IX_itens_pedidos_produto_id");

            migrationBuilder.RenameIndex(
                name: "IX_itens_pedido_PedidoId",
                table: "itens_pedidos",
                newName: "IX_itens_pedidos_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_cliente_telefone",
                table: "clientes",
                newName: "IX_clientes_telefone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itens_pedidos",
                table: "itens_pedidos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoId",
                table: "itens_pedidos",
                column: "PedidoId",
                principalTable: "pedidos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedidos_produtos_produto_id",
                table: "itens_pedidos",
                column: "produto_id",
                principalTable: "produtos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_clientes_cliente_id",
                table: "pedidos",
                column: "cliente_id",
                principalTable: "clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedidos_pedidos_PedidoId",
                table: "itens_pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_itens_pedidos_produtos_produto_id",
                table: "itens_pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_clientes_cliente_id",
                table: "pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itens_pedidos",
                table: "itens_pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.RenameTable(
                name: "itens_pedidos",
                newName: "itens_pedido");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "cliente");

            migrationBuilder.RenameIndex(
                name: "IX_itens_pedidos_produto_id",
                table: "itens_pedido",
                newName: "IX_itens_pedido_produto_id");

            migrationBuilder.RenameIndex(
                name: "IX_itens_pedidos_PedidoId",
                table: "itens_pedido",
                newName: "IX_itens_pedido_PedidoId");

            migrationBuilder.RenameIndex(
                name: "IX_clientes_telefone",
                table: "cliente",
                newName: "IX_cliente_telefone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itens_pedido",
                table: "itens_pedido",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cliente",
                table: "cliente",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedido_pedidos_PedidoId",
                table: "itens_pedido",
                column: "PedidoId",
                principalTable: "pedidos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_itens_pedido_produtos_produto_id",
                table: "itens_pedido",
                column: "produto_id",
                principalTable: "produtos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_cliente_cliente_id",
                table: "pedidos",
                column: "cliente_id",
                principalTable: "cliente",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
