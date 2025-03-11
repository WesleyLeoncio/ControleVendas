using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class AddNovasConfiguracoesEmPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status_pedido",
                table: "pedidos",
                newName: "status");

            migrationBuilder.AlterColumn<string>(
                name: "forma_pagamento",
                table: "pedidos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<decimal>(
                name: "valor_pago",
                table: "pedidos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "valor_pago",
                table: "pedidos");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "pedidos",
                newName: "status_pedido");

            migrationBuilder.AlterColumn<string>(
                name: "forma_pagamento",
                table: "pedidos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
