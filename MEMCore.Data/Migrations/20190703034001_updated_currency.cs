using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMCore.Data.Migrations
{
    public partial class updated_currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_GetCurrencies_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetCurrencies",
                table: "GetCurrencies");

            migrationBuilder.RenameTable(
                name: "GetCurrencies",
                newName: "Currencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Currencies_CurrencyId",
                table: "Expenses",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Currencies_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "GetCurrencies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GetCurrencies",
                table: "GetCurrencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_GetCurrencies_CurrencyId",
                table: "Expenses",
                column: "CurrencyId",
                principalTable: "GetCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
