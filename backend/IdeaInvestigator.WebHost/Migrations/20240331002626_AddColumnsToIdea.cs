using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdeaInvestigator.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToIdea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Ideas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Ideas");
        }
    }
}
