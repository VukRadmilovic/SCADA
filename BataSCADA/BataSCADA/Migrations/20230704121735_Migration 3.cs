using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BataSCADA.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalogInputs_DigitalInputs_TagName",
                table: "AnalogInputs");

            migrationBuilder.AddColumn<int>(
                name: "Driver",
                table: "AnalogInputs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsOn",
                table: "AnalogInputs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ScanTime",
                table: "AnalogInputs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AnalogInputs_Tags_TagName",
                table: "AnalogInputs",
                column: "TagName",
                principalTable: "Tags",
                principalColumn: "TagName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnalogInputs_Tags_TagName",
                table: "AnalogInputs");

            migrationBuilder.DropColumn(
                name: "Driver",
                table: "AnalogInputs");

            migrationBuilder.DropColumn(
                name: "IsOn",
                table: "AnalogInputs");

            migrationBuilder.DropColumn(
                name: "ScanTime",
                table: "AnalogInputs");

            migrationBuilder.AddForeignKey(
                name: "FK_AnalogInputs_DigitalInputs_TagName",
                table: "AnalogInputs",
                column: "TagName",
                principalTable: "DigitalInputs",
                principalColumn: "TagName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
