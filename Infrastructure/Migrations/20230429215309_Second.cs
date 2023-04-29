using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reaction_AspNetUsers_UserId",
                table: "Reaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Reaction_Posts_PostId",
                table: "Reaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reaction",
                table: "Reaction");

            migrationBuilder.RenameTable(
                name: "Reaction",
                newName: "Reactions");

            migrationBuilder.RenameIndex(
                name: "IX_Reaction_UserId",
                table: "Reactions",
                newName: "IX_Reactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reaction_PostId",
                table: "Reactions",
                newName: "IX_Reactions_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_AspNetUsers_UserId",
                table: "Reactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Posts_PostId",
                table: "Reactions",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_AspNetUsers_UserId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Posts_PostId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

            migrationBuilder.RenameTable(
                name: "Reactions",
                newName: "Reaction");

            migrationBuilder.RenameIndex(
                name: "IX_Reactions_UserId",
                table: "Reaction",
                newName: "IX_Reaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reactions_PostId",
                table: "Reaction",
                newName: "IX_Reaction_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reaction",
                table: "Reaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reaction_AspNetUsers_UserId",
                table: "Reaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reaction_Posts_PostId",
                table: "Reaction",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
