using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    public partial class Relations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherMeasurementId",
                table: "WeatherPredictions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId",
                principalTable: "WeatherMeasurements",
                principalColumn: "WeatherMeasurementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherMeasurementId",
                table: "WeatherPredictions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
