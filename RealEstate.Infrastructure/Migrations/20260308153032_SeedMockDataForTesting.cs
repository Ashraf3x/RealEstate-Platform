using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RealEstate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMockDataForTesting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyId", "CreatedAt", "Description", "Location", "PricePerShare", "Status", "Title", "TotalShares" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "A modern smart apartment with 3D viewing.", "New Cairo", 50m, "Active", "Smart Apartment in New Cairo", 1000 },
                    { 2, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Premium office space in the administrative capital.", "New Capital", 100m, "Active", "Fractional Real Estate Office", 500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "PropertyId",
                keyValue: 2);
        }
    }
}
