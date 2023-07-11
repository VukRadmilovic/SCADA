using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BataSCADA.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SnoozedUntil",
                table: "Alarms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Triggered",
                table: "Alarms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SnoozedUntil",
                table: "Alarms");

            migrationBuilder.DropColumn(
                name: "Triggered",
                table: "Alarms");
        }
    }
}
