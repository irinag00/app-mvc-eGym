using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eGym.Migrations
{
    /// <inheritdoc />
    public partial class itemAgregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idRopa_Color",
                table: "Ropa_Colores");

            migrationBuilder.AddColumn<string>(
                name: "linkElemento",
                table: "Ropas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "linkElemento",
                table: "Ropas");

            migrationBuilder.AddColumn<int>(
                name: "idRopa_Color",
                table: "Ropa_Colores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
