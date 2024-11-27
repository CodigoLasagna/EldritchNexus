using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Migrations
{
    /// <inheritdoc />
    public partial class userTestTeamsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_UsersId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_UsersId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Teams");

            migrationBuilder.CreateTable(
                name: "TeamUser",
                columns: table => new
                {
                    TeamsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUser", x => new { x.TeamsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TeamUser_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUser_UsersId",
                table: "TeamUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamUser");

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UsersId",
                table: "Teams",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_UsersId",
                table: "Teams",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
