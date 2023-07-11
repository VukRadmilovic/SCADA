using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BataSCADA.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alarms_AnalogInputs_AnalogInputTagName",
                table: "Alarms");

            migrationBuilder.DropIndex(
                name: "IX_Alarms_AnalogInputTagName",
                table: "Alarms");

            migrationBuilder.AlterColumn<string>(
                name: "AnalogInputTagName",
                table: "Alarms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnalogInputTagName",
                table: "Alarms",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputTagName",
                table: "Alarms",
                column: "AnalogInputTagName");

            migrationBuilder.AddForeignKey(
                name: "FK_Alarms_AnalogInputs_AnalogInputTagName",
                table: "Alarms",
                column: "AnalogInputTagName",
                principalTable: "AnalogInputs",
                principalColumn: "TagName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
