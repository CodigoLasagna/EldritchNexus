using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helper.Migrations
{
    /// <inheritdoc />
    public partial class testUserTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_UserId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Teams",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                newName: "IX_Teams_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_UsersId",
                table: "Teams",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_UsersId",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Teams",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_UsersId",
                table: "Teams",
                newName: "IX_Teams_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_UserId",
                table: "Teams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
