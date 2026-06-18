using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComponentesComputadoras.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDniToClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNI",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNI",
                table: "Clientes");
        }
    }
}
