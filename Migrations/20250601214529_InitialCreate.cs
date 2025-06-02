using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmbalagemPedidos.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasEmbalagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasEmbalagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasEmbalagem_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dimensoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Largura = table.Column<int>(type: "int", nullable: false),
                    Comprimento = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dimensoes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Caixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaixaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DimensoesId = table.Column<int>(type: "int", nullable: false),
                    DimensoesId1 = table.Column<int>(type: "int", nullable: false),
                    Produtos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespostaEmbalagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caixas_Dimensoes_DimensoesId",
                        column: x => x.DimensoesId,
                        principalTable: "Dimensoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Caixas_Dimensoes_DimensoesId1",
                        column: x => x.DimensoesId1,
                        principalTable: "Dimensoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caixas_RespostasEmbalagem_RespostaEmbalagemId",
                        column: x => x.RespostaEmbalagemId,
                        principalTable: "RespostasEmbalagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_DimensoesId",
                table: "Caixas",
                column: "DimensoesId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_DimensoesId1",
                table: "Caixas",
                column: "DimensoesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Caixas_RespostaEmbalagemId",
                table: "Caixas",
                column: "RespostaEmbalagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Dimensoes_ProdutoId",
                table: "Dimensoes",
                column: "ProdutoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_PedidoId",
                table: "Produtos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasEmbalagem_PedidoId",
                table: "RespostasEmbalagem",
                column: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixas");

            migrationBuilder.DropTable(
                name: "Dimensoes");

            migrationBuilder.DropTable(
                name: "RespostasEmbalagem");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
