using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramTimeManagementBot.Migrations
{
    public partial class _3008Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastMessage",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "LastCommandQuery",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessageTime",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeSpent",
                table: "UserActions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "UserActions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCommandQuery",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastMessageTime",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMessage",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeSpent",
                table: "UserActions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "ActivityId",
                table: "UserActions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
