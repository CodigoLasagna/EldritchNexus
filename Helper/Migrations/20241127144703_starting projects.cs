using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Migrations
{
    /// <inheritdoc />
    public partial class startingprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GitRepositoryPath",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitRepositoryPath",
                table: "Projects");
        }
    }
}
