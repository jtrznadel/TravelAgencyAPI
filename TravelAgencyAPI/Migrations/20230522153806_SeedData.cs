using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelAgencyAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "Country", "Description", "DestinationPoint", "EndDate", "Name", "Price", "StartDate", "TourLimit" },
                values: new object[,]
                {
                    { 1, "Italy", "History and food combine on this Handcrafted seven-night tour. You’ll visit mountain-top towns, taste everything from pastries to cannoli pasta, and stand among some of the most significant Greek ruins outside of Greece.", "Sicily", new DateTime(2023, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spotlight on Sicily", 1200.0, new DateTime(2023, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 },
                    { 2, "Italy", "This seven-night tour showcases some of Puglia’s prettiest sites, from vineyard-side villages to lofty cathedrals. Whitewashed Alberobello is home for your first five nights, then it’s off to Lecce.", "Puglia", new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Puglia - Italy's undiscovered heel", 1120.0, new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
