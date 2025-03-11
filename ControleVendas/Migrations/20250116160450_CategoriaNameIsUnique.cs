using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_vendas.Migrations
{
    /// <inheritdoc />
    public partial class CategoriaNameIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_categorias_nome",
                table: "categorias",
                column: "nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_categorias_nome",
                table: "categorias");
        }
    }
}
