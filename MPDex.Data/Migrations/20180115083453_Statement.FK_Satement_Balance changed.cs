using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MPDex.Data.Migrations
{
    public partial class StatementFK_Satement_Balancechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statement_Balance_BalanceCustomerId_BalanceCoinId",
                table: "Statement");

            migrationBuilder.DropIndex(
                name: "IX_Statement_CustomerId",
                table: "Statement");

            migrationBuilder.DropIndex(
                name: "IX_Statement_BalanceCustomerId_BalanceCoinId",
                table: "Statement");

            migrationBuilder.DropColumn(
                name: "BalanceCoinId",
                table: "Statement");

            migrationBuilder.DropColumn(
                name: "BalanceCustomerId",
                table: "Statement");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_CustomerId_CoinId",
                table: "Statement",
                columns: new[] { "CustomerId", "CoinId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Statement_Balance_CustomerId_CoinId",
                table: "Statement",
                columns: new[] { "CustomerId", "CoinId" },
                principalTable: "Balance",
                principalColumns: new[] { "CustomerId", "CoinId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statement_Balance_CustomerId_CoinId",
                table: "Statement");

            migrationBuilder.DropIndex(
                name: "IX_Statement_CustomerId_CoinId",
                table: "Statement");

            migrationBuilder.AddColumn<short>(
                name: "BalanceCoinId",
                table: "Statement",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<Guid>(
                name: "BalanceCustomerId",
                table: "Statement",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Statement_CustomerId",
                table: "Statement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_BalanceCustomerId_BalanceCoinId",
                table: "Statement",
                columns: new[] { "BalanceCustomerId", "BalanceCoinId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Statement_Balance_BalanceCustomerId_BalanceCoinId",
                table: "Statement",
                columns: new[] { "BalanceCustomerId", "BalanceCoinId" },
                principalTable: "Balance",
                principalColumns: new[] { "CustomerId", "CoinId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
