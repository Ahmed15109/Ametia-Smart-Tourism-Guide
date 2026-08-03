using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grad.Migrations
{
    /// <inheritdoc />
    public partial class upDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tourismt_Places");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Restaurants");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "transportProviders",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Tourismt_Places",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Restaurants",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Hotels",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "EntertainmentPlaces",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Embasses",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Banks",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "transportProviders");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Tourismt_Places");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "EntertainmentPlaces");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Embasses");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Banks");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Tourismt_Places",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
