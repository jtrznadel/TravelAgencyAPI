using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelAgencyAPI.Migrations
{
    /// <inheritdoc />
    public partial class TourCreatedByIdAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedById",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedById",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CreatedById",
                table: "Tours",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Users_CreatedById",
                table: "Tours",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Users_CreatedById",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_CreatedById",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tours");
        }
    }
}
