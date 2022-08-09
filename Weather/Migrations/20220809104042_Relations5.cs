using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    public partial class Relations5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.RenameColumn(
                name: "WeatherMeasurementId",
                table: "WeatherPredictions",
                newName: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_ParentId",
                table: "WeatherPredictions",
                column: "ParentId",
                principalTable: "WeatherMeasurements",
                principalColumn: "WeatherMeasurementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_ParentId",
                table: "WeatherPredictions");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "WeatherPredictions",
                newName: "WeatherMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId",
                principalTable: "WeatherMeasurements",
                principalColumn: "WeatherMeasurementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
