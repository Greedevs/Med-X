using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedX.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoomTableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Rooms",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "Rooms",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Place",
                table: "Rooms",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
