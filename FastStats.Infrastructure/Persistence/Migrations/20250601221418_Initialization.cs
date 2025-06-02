using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastStats.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticalComputations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DatasetId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComputationStrategyIdentifier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Median = table.Column<decimal>(type: "numeric", nullable: true),
                    Variance = table.Column<decimal>(type: "numeric", nullable: true),
                    Average = table.Column<decimal>(type: "numeric", nullable: true),
                    ComputedStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ComputedEndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticalComputations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticalComputations_Datasets_DatasetId",
                        column: x => x.DatasetId,
                        principalTable: "Datasets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticalComputations_DatasetId",
                table: "StatisticalComputations",
                column: "DatasetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticalComputations");

            migrationBuilder.DropTable(
                name: "Datasets");
        }
    }
}
