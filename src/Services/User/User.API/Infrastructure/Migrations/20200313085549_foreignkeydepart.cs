using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.API.Infrastructure.Migrations
{
    public partial class foreignkeydepart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Information_UserStatus_userStatusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropIndex(
                name: "IX_User_Information_userStatusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.AlterColumn<long>(
                name: "userStatusId",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updateTime",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthdate",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statusId",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Information_statusId",
                schema: "PeopleManager",
                table: "User_Information",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Information_UserStatus_statusId",
                schema: "PeopleManager",
                table: "User_Information",
                column: "statusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Information_UserStatus_statusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropIndex(
                name: "IX_User_Information_statusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "statusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updateTime",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createtime",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthdate",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "userStatusId",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_User_Information_userStatusId",
                schema: "PeopleManager",
                table: "User_Information",
                column: "userStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Information_UserStatus_userStatusId",
                schema: "PeopleManager",
                table: "User_Information",
                column: "userStatusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
