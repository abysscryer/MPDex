using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MPDex.Data.Migrations
{
    public partial class BookForignKeyNotNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Coin_CoinId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Customer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Coin",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Book",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "CoinId",
                table: "Book",
                nullable: false,
                oldClrType: typeof(short),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Coin_CoinId",
                table: "Book",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Coin_CoinId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Coin");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Book",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<short>(
                name: "CoinId",
                table: "Book",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Coin_CoinId",
                table: "Book",
                column: "CoinId",
                principalTable: "Coin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Customer_CustomerId",
                table: "Book",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
