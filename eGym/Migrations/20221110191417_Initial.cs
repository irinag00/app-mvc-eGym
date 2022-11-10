using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGym.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Colores",
                columns: table => new
                {
                    idColor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagenColor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colores", x => x.idColor);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    idMarca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagenMarca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.idMarca);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    idTienda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    resumen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagenTienda = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.idTienda);
                });

            migrationBuilder.CreateTable(
                name: "Ropas",
                columns: table => new
                {
                    idRopa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    detalles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<double>(type: "float", nullable: false),
                    imagenRopa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marcaId = table.Column<int>(type: "int", nullable: false),
                    tiendaId = table.Column<int>(type: "int", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ropas", x => x.idRopa);
                    table.ForeignKey(
                        name: "FK_Ropas_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ropas_Marcas_marcaId",
                        column: x => x.marcaId,
                        principalTable: "Marcas",
                        principalColumn: "idMarca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ropas_Tiendas_tiendaId",
                        column: x => x.tiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "idTienda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ropa_Colores",
                columns: table => new
                {
                    idRopa = table.Column<int>(type: "int", nullable: false),
                    idColor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ropa_Colores", x => new { x.idRopa, x.idColor });
                    table.ForeignKey(
                        name: "FK_Ropa_Colores_Colores_idColor",
                        column: x => x.idColor,
                        principalTable: "Colores",
                        principalColumn: "idColor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ropa_Colores_Ropas_idRopa",
                        column: x => x.idRopa,
                        principalTable: "Ropas",
                        principalColumn: "idRopa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ropa_Colores_idColor",
                table: "Ropa_Colores",
                column: "idColor");

            migrationBuilder.CreateIndex(
                name: "IX_Ropas_categoriaId",
                table: "Ropas",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ropas_marcaId",
                table: "Ropas",
                column: "marcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ropas_tiendaId",
                table: "Ropas",
                column: "tiendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ropa_Colores");

            migrationBuilder.DropTable(
                name: "Colores");

            migrationBuilder.DropTable(
                name: "Ropas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Tiendas");
        }
    }
}
