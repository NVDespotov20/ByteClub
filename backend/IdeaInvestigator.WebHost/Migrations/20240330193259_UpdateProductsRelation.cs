using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaInvestigator.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    AdvertPlatforms = table.Column<string>(type: "text", nullable: false),
                    TargetAudience = table.Column<string>(type: "text", nullable: false),
                    Budget = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    NumberOfCampaigns = table.Column<int>(type: "integer", nullable: false),
                    Suggestion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ideas_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ideas_CreatorId",
                table: "Ideas",
                column: "CreatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ideas");
        }
    }
}
