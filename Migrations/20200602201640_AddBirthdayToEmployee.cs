using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeDatabase.Migrations
{
    public partial class AddBirthdayToEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextBirthday",
                table: "Employee",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextBirthday",
                table: "Employee");
        }
    }
}
