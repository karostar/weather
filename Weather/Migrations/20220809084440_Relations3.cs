using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    public partial class Relations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherPredictions",
                table: "WeatherPredictions");

            migrationBuilder.DropIndex(
                name: "IX_WeatherPredictions_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.RenameColumn(
                name: "WeatherPredictionId",
                table: "WeatherPredictions",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherMeasurementId",
                table: "WeatherPredictions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherPredictions",
                table: "WeatherPredictions",
                columns: new[] { "WeatherMeasurementId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId",
                principalTable: "WeatherMeasurements",
                principalColumn: "WeatherMeasurementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherPredictions",
                table: "WeatherPredictions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "WeatherPredictions",
                newName: "WeatherPredictionId");

            migrationBuilder.AlterColumn<int>(
                name: "WeatherMeasurementId",
                table: "WeatherPredictions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherPredictions",
                table: "WeatherPredictions",
                column: "WeatherPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherPredictions_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherPredictions_WeatherMeasurements_WeatherMeasurementId",
                table: "WeatherPredictions",
                column: "WeatherMeasurementId",
                principalTable: "WeatherMeasurements",
                principalColumn: "WeatherMeasurementId");
        }
    }
}
