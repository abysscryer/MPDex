using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MPDex.Data.Migrations
{
    public partial class BookBookTypeChangedtoBookOrderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookType",
                table: "Book");

            migrationBuilder.AddColumn<byte>(
                name: "OrderType",
                table: "Book",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "Book");

            migrationBuilder.AddColumn<byte>(
                name: "BookType",
                table: "Book",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
