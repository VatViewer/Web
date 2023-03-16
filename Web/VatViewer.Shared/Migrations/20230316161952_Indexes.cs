using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VatViewer.Shared.Migrations
{
    /// <inheritdoc />
    public partial class Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Positions_Latitude",
                table: "Positions",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Longitude",
                table: "Positions",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_Aircraft",
                table: "FlightPlans",
                column: "Aircraft");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_Arrival",
                table: "FlightPlans",
                column: "Arrival");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_Departure",
                table: "FlightPlans",
                column: "Departure");

            migrationBuilder.CreateIndex(
                name: "IX_Atis_Callsign",
                table: "Atis",
                column: "Callsign");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Positions_Latitude",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_Longitude",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_FlightPlans_Aircraft",
                table: "FlightPlans");

            migrationBuilder.DropIndex(
                name: "IX_FlightPlans_Arrival",
                table: "FlightPlans");

            migrationBuilder.DropIndex(
                name: "IX_FlightPlans_Departure",
                table: "FlightPlans");

            migrationBuilder.DropIndex(
                name: "IX_Atis_Callsign",
                table: "Atis");
        }
    }
}
