using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grad.Migrations
{
    /// <inheritdoc />
    public partial class up301 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Embasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Embasses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
