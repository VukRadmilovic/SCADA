using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BataSCADA.Migrations
{
    /// <inheritdoc />
    public partial class Migration56 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Value",
                table: "DigitalOutputs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "AnalogOutputs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "DigitalOutputs");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "AnalogOutputs");
        }
    }
}
