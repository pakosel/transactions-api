using Microsoft.EntityFrameworkCore.Migrations;

namespace transactions_api.Migrations
{
    public partial class NewFieldsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AmountCurrencySymbol",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Commision",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Jsondata",
                table: "Transactions",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Market",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operation",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrencySymbol",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "Transactions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AmountCurrencySymbol",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Commision",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Jsondata",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Market",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Operation",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PriceCurrencySymbol",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transactions");
        }
    }
}
