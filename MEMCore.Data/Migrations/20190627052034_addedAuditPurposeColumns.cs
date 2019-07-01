using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMCore.Data.Migrations
{
    public partial class addedAuditPurposeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "inDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 714, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.AddColumn<DateTime>(
                name: "updateDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 724, DateTimeKind.Utc).AddTicks(2479));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inDate",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "updateDate",
                table: "Expenses");
        }
    }
}
