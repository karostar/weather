using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    public partial class WindStrength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WindStrength",
                table: "WeatherMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WindStrength",
                table: "WeatherMeasurements");
        }
    }
}
