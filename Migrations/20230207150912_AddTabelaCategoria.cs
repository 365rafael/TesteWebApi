using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteWebApi.Migrations
{
    public partial class AddTabelaCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
