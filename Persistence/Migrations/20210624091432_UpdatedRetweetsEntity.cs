using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdatedRetweetsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_AspNetUsers_AppUserId",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Retweets_AppUserId",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Retweets");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Retweets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Retweets",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_AppUserId1",
                table: "Retweets",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_AspNetUsers_AppUserId1",
                table: "Retweets",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Retweets_AspNetUsers_AppUserId1",
                table: "Retweets");

            migrationBuilder.DropIndex(
                name: "IX_Retweets_AppUserId1",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Retweets");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Retweets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Retweets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Retweets_AppUserId",
                table: "Retweets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Retweets_AspNetUsers_AppUserId",
                table: "Retweets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
