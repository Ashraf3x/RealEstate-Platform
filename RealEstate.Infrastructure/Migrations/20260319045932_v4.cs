using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AnnualYield",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppreciationProgress",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppreciationStatus",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OccupancyRate",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 1,
                columns: new[] { "AnnualYield", "AppreciationProgress", "AppreciationStatus", "OccupancyRate" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 2,
                columns: new[] { "AnnualYield", "AppreciationProgress", "AppreciationStatus", "OccupancyRate" },
                values: new object[] { null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualYield",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AppreciationProgress",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AppreciationStatus",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OccupancyRate",
                table: "Properties");
        }
    }
}
