using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MPDex.Data.Migrations
{
    public partial class Initialcreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 16, nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    CellPhone = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    FamilyName = table.Column<string>(maxLength: 16, nullable: false),
                    GivenName = table.Column<string>(maxLength: 16, nullable: false),
                    NickName = table.Column<string>(maxLength: 16, nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fee",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoinId = table.Column<short>(nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Percent = table.Column<decimal>(type: "decimal(3, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fee_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Amount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    CoinId = table.Column<short>(nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Price = table.Column<decimal>(type: "decimal(20, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    CoinId = table.Column<short>(nullable: false),
                    BookAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => new { x.CustomerId, x.CoinId });
                    table.ForeignKey(
                        name: "FK_Balance_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Balance_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Amount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    BookStatus = table.Column<byte>(nullable: false),
                    CoinId = table.Column<short>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    IPAddress = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OnUpdated = table.Column<DateTime>(nullable: true),
                    OrderType = table.Column<byte>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(20, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Amount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    TradeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    CoinId = table.Column<short>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OrderType = table.Column<byte>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(20, 8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AfterAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    BalanceCoinId = table.Column<short>(nullable: false),
                    BalanceCustomerId = table.Column<Guid>(nullable: false),
                    BeforeAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    CoinId = table.Column<short>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    FeeAmount = table.Column<decimal>(type: "decimal(20, 8)", nullable: false),
                    FeeId = table.Column<short>(nullable: true),
                    IPAddress = table.Column<string>(unicode: false, maxLength: 36, nullable: false),
                    OnCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OnUpdated = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    StatementType = table.Column<byte>(nullable: false),
                    VerifyKey = table.Column<string>(unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statement_Coin_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statement_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statement_Fee_FeeId",
                        column: x => x.FeeId,
                        principalTable: "Fee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statement_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statement_Balance_BalanceCustomerId_BalanceCoinId",
                        columns: x => new { x.BalanceCustomerId, x.BalanceCoinId },
                        principalTable: "Balance",
                        principalColumns: new[] { "CustomerId", "CoinId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_CoinId",
                table: "Balance",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CoinId",
                table: "Book",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CustomerId",
                table: "Book",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_TradeId",
                table: "Contract",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Fee_CoinId",
                table: "Fee",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CoinId",
                table: "Order",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ContractId",
                table: "Order",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_CoinId",
                table: "Statement",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_CustomerId",
                table: "Statement",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_FeeId",
                table: "Statement",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Statement_OrderId",
                table: "Statement",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statement_BalanceCustomerId_BalanceCoinId",
                table: "Statement",
                columns: new[] { "BalanceCustomerId", "BalanceCoinId" });

            migrationBuilder.CreateIndex(
                name: "IX_Trade_CoinId",
                table: "Trade",
                column: "CoinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Statement");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Fee");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Coin");
        }
    }
}
