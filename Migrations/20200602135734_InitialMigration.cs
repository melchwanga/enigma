using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeDatabase.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 3, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LookUpData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemType = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCounty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    CountyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCounty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCounty_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    MaritalStatusId = table.Column<int>(nullable: false),
                    SubCountyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_LookUpData_GenderId",
                        column: x => x.GenderId,
                        principalTable: "LookUpData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employee_LookUpData_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "LookUpData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employee_SubCounty_SubCountyID",
                        column: x => x.SubCountyID,
                        principalTable: "SubCounty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_GenderId",
                table: "Employee",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_MaritalStatusId",
                table: "Employee",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_SubCountyID",
                table: "Employee",
                column: "SubCountyID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCounty_CountyId",
                table: "SubCounty",
                column: "CountyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "LookUpData");

            migrationBuilder.DropTable(
                name: "SubCounty");

            migrationBuilder.DropTable(
                name: "County");
        }
    }
}
