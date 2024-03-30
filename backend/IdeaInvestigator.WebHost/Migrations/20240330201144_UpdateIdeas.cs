using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaInvestigator.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdeas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suggestion",
                table: "Ideas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Suggestion",
                table: "Ideas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
