using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMCore.Data.Migrations
{
    public partial class addedpaymentmethodasenum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Expenses");
        }
    }
}
