using Microsoft.EntityFrameworkCore.Migrations;

namespace User.API.Infrastructure.Migrations
{
    public partial class userinformationkeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInformation_UserStatus_statusId",
                table: "UserInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation");

            migrationBuilder.DropIndex(
                name: "IX_UserInformation_statusId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "statusId",
                table: "UserInformation");

            migrationBuilder.EnsureSchema(
                name: "PeopleManager");

            migrationBuilder.RenameTable(
                name: "UserInformation",
                newName: "User_Information",
                newSchema: "PeopleManager");

            migrationBuilder.AddColumn<int>(
                name: "userStatusId",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "address_City",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_County",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_Province",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_Street",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_Zip",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createUserInfo__createUserId",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createUserInfo__createUserName",
                schema: "PeopleManager",
                table: "User_Information",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Information",
                schema: "PeopleManager",
                table: "User_Information",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Information_UserStatus_userStatusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Information",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropIndex(
                name: "IX_User_Information_userStatusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "userStatusId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "address_City",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "address_County",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "address_Province",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "address_Street",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "address_Zip",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "createUserInfo__createUserId",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.DropColumn(
                name: "createUserInfo__createUserName",
                schema: "PeopleManager",
                table: "User_Information");

            migrationBuilder.RenameTable(
                name: "User_Information",
                schema: "PeopleManager",
                newName: "UserInformation");

            migrationBuilder.AddColumn<int>(
                name: "statusId",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInformation",
                table: "UserInformation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_statusId",
                table: "UserInformation",
                column: "statusId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInformation_UserStatus_statusId",
                table: "UserInformation",
                column: "statusId",
                principalTable: "UserStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
