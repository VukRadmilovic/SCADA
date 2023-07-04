using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BataSCADA.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagName);
                });

            migrationBuilder.CreateTable(
                name: "AnalogOutputs",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InitialValue = table.Column<double>(type: "float", nullable: false),
                    LowLimit = table.Column<double>(type: "float", nullable: false),
                    HighLimit = table.Column<double>(type: "float", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogOutputs", x => x.TagName);
                    table.ForeignKey(
                        name: "FK_AnalogOutputs_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalInputs",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Driver = table.Column<int>(type: "int", nullable: false),
                    ScanTime = table.Column<int>(type: "int", nullable: false),
                    IsOn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalInputs", x => x.TagName);
                    table.ForeignKey(
                        name: "FK_DigitalInputs_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalOutputs",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InitialValue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOutputs", x => x.TagName);
                    table.ForeignKey(
                        name: "FK_DigitalOutputs_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalogInputs",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LowLimit = table.Column<double>(type: "float", nullable: false),
                    HighLimit = table.Column<double>(type: "float", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogInputs", x => x.TagName);
                    table.ForeignKey(
                        name: "FK_AnalogInputs_DigitalInputs_TagName",
                        column: x => x.TagName,
                        principalTable: "DigitalInputs",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Limit = table.Column<double>(type: "float", nullable: false),
                    AnalogInputTagName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_AnalogInputs_AnalogInputTagName",
                        column: x => x.AnalogInputTagName,
                        principalTable: "AnalogInputs",
                        principalColumn: "TagName");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputTagName",
                table: "Alarms",
                column: "AnalogInputTagName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "AnalogOutputs");

            migrationBuilder.DropTable(
                name: "DigitalOutputs");

            migrationBuilder.DropTable(
                name: "AnalogInputs");

            migrationBuilder.DropTable(
                name: "DigitalInputs");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
