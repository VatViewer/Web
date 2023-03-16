using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VatViewer.Shared.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    Facility = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    TextAtisRaw = table.Column<string>(type: "text", nullable: false),
                    LogonTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogoffTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    Facility = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ControllerInfoRaw = table.Column<string>(type: "text", nullable: false),
                    LogonTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogoffTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Length = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<int>(type: "integer", nullable: false),
                    FlightRules = table.Column<string>(type: "text", nullable: false),
                    Aircraft = table.Column<string>(type: "text", nullable: false),
                    Departure = table.Column<string>(type: "text", nullable: false),
                    Arrival = table.Column<string>(type: "text", nullable: false),
                    Route = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    FlightPlanId = table.Column<int>(type: "integer", nullable: true),
                    LogonTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogoffTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Length = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pilots_FlightPlans_FlightPlanId",
                        column: x => x.FlightPlanId,
                        principalTable: "FlightPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prefliles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cid = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Callsign = table.Column<string>(type: "text", nullable: false),
                    FlightPlanId = table.Column<int>(type: "integer", nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefliles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prefliles_FlightPlans_FlightPlanId",
                        column: x => x.FlightPlanId,
                        principalTable: "FlightPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PilotId = table.Column<int>(type: "integer", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Altitude = table.Column<int>(type: "integer", nullable: false),
                    GroundSpeed = table.Column<int>(type: "integer", nullable: false),
                    Heading = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atis_Cid",
                table: "Atis",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Atis_LogonTime",
                table: "Atis",
                column: "LogonTime");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_Callsign",
                table: "Controllers",
                column: "Callsign");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_Cid",
                table: "Controllers",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_Frequency",
                table: "Controllers",
                column: "Frequency");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_LogonTime",
                table: "Controllers",
                column: "LogonTime");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_Cid",
                table: "FlightPlans",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPlans_Timestamp",
                table: "FlightPlans",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_Callsign",
                table: "Pilots",
                column: "Callsign");

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_Cid",
                table: "Pilots",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_FlightPlanId",
                table: "Pilots",
                column: "FlightPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_LogoffTime",
                table: "Pilots",
                column: "LogoffTime");

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_LogonTime",
                table: "Pilots",
                column: "LogonTime");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PilotId",
                table: "Positions",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Prefliles_Cid",
                table: "Prefliles",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Prefliles_FlightPlanId",
                table: "Prefliles",
                column: "FlightPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atis");

            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Prefliles");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "FlightPlans");
        }
    }
}
