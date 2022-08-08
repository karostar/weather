using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherPredictions",
                columns: table => new
                {
                    WeatherPredictionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemperatureCPrediction = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    WeatherMeasurementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherPredictions", x => x.WeatherPredictionId);
                    table.ForeignKey(
                        name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                        column: x => x.WeatherMeasurementId,
                        principalTable: "WeatherMeasurements",
                        principalColumn: "WeatherMeasurementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherPredictions_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherPredictions");
        }
    }
}
