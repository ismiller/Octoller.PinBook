using Microsoft.EntityFrameworkCore.Migrations;

namespace Octoller.PinBook.Web.Data.Migrations
{
    public partial class ModelConfigurationChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_AspNetUsers_UserId1",
                table: "BookLists");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BookLists_UserId",
                table: "BookLists");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "BookLists",
                newName: "ProfileId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookLists",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLists_UserId1",
                table: "BookLists",
                newName: "IX_BookLists_ProfileId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BookLists_ProfileId",
                table: "BookLists",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_Profiles_ProfileId1",
                table: "BookLists",
                column: "ProfileId1",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_Profiles_ProfileId1",
                table: "BookLists");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BookLists_ProfileId",
                table: "BookLists");

            migrationBuilder.RenameColumn(
                name: "ProfileId1",
                table: "BookLists",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "BookLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLists_ProfileId1",
                table: "BookLists",
                newName: "IX_BookLists_UserId1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BookLists_UserId",
                table: "BookLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_AspNetUsers_UserId1",
                table: "BookLists",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
