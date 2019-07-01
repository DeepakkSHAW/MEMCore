using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMCore.Data.Migrations
{
    public partial class updatedAuditPurposeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updateDate",
                table: "Expenses",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 724, DateTimeKind.Utc).AddTicks(2479));

            migrationBuilder.AlterColumn<DateTime>(
                name: "inDate",
                table: "Expenses",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 714, DateTimeKind.Utc).AddTicks(7632));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updateDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 724, DateTimeKind.Utc).AddTicks(2479),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "inDate",
                table: "Expenses",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 27, 5, 20, 33, 714, DateTimeKind.Utc).AddTicks(7632),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getutcdate()");
        }
    }
}
