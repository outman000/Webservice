using Microsoft.EntityFrameworkCore.Migrations;

namespace User.API.Infrastructure.Migrations
{
    public partial class userinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "birthdate",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mobileCall",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phoneCall",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "UserInformation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userPwd",
                table: "UserInformation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthdate",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "description",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "email",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "mobileCall",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "phoneCall",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "UserInformation");

            migrationBuilder.DropColumn(
                name: "userPwd",
                table: "UserInformation");
        }
    }
}
