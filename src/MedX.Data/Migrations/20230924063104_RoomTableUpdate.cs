using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedX.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FemaleCount",
                table: "Rooms",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaleCount",
                table: "Rooms",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FemaleCount",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MaleCount",
                table: "Rooms");
        }
    }
}
