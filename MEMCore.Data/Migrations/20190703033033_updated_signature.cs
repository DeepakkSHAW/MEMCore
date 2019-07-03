using Microsoft.EntityFrameworkCore.Migrations;

namespace MEMCore.Data.Migrations
{
    public partial class updated_signature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Currency_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "GetCurrencies");

            migrationBuilder.RenameColumn(
                name: "signature",
                table: "Expenses",
                newName: "Signature");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_GetCurrencies_CurrencyId",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GetCurrencies",
                table: "GetCurrencies");

            migrationBuilder.RenameTable(
                name: "GetCurrencies",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "Expenses",
                newName: "signature");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Currency_CurrencyId",
                table: "Expenses",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
