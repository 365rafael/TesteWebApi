using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteWebApi.Migrations
{
    public partial class AddTabelaPedidoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PedidoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItems");
        }
    }
}
