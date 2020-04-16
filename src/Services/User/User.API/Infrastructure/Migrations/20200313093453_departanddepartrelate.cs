using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace User.API.Infrastructure.Migrations
{
    public partial class departanddepartrelate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_Organization",
                schema: "PeopleManager",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    orgName = table.Column<string>(nullable: true),
                    orgCode = table.Column<string>(nullable: true),
                    orgParentcode = table.Column<long>(nullable: false),
                    orgType = table.Column<string>(nullable: true),
                    unitId = table.Column<string>(nullable: true),
                    unionType = table.Column<string>(nullable: true),
                    orgPath = table.Column<string>(nullable: true),
                    orgSequence = table.Column<string>(nullable: true),
                    orgLevel = table.Column<string>(nullable: true),
                    orgGlobalNo = table.Column<string>(nullable: true),
                    createUserInfo__createUserId = table.Column<string>(nullable: true),
                    createUserInfo__createUserName = table.Column<string>(nullable: true),
                    departparentId = table.Column<long>(nullable: false),
                    departInformationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Organization_User_Organization_departInformationId",
                        column: x => x.departInformationId,
                        principalSchema: "PeopleManager",
                        principalTable: "User_Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserOrganization_Relate",
                schema: "PeopleManager",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userid = table.Column<long>(nullable: false),
                    orgid = table.Column<long>(nullable: false),
                    descript = table.Column<string>(nullable: true),
                    relatetype = table.Column<string>(nullable: true),
                    createadate = table.Column<DateTime>(nullable: true),
                    updatedate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization_Relate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Organization_departInformationId",
                schema: "PeopleManager",
                table: "User_Organization",
                column: "departInformationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_Organization",
                schema: "PeopleManager");

            migrationBuilder.DropTable(
                name: "UserOrganization_Relate",
                schema: "PeopleManager");
        }
    }
}
