using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeveloperStore.Sales.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_NAME = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SALE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    SALE_DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BRANCH_NAME = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TOTAL_AMOUNT = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TOTAL_DISCOUNT = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IS_CANCELLED = table.Column<bool>(type: "boolean", nullable: false),
                    CUSTOMER_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SALE_Customers_CUSTOMER_ID",
                        column: x => x.CUSTOMER_ID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SALE_ITEM",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    QUANTITY = table.Column<int>(type: "integer", nullable: false),
                    DISCOUNT = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IS_CANCELLED = table.Column<bool>(type: "boolean", nullable: false),
                    SALE_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    PRODUCT_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALE_ITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SALE_ITEM_PRODUCT_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SALE_ITEM_SALE_SALE_ID",
                        column: x => x.SALE_ID,
                        principalTable: "SALE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SALE_CUSTOMER_ID",
                table: "SALE",
                column: "CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALE_ITEM_PRODUCT_ID",
                table: "SALE_ITEM",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SALE_ITEM_SALE_ID",
                table: "SALE_ITEM",
                column: "SALE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SALE_ITEM");

            migrationBuilder.DropTable(
                name: "PRODUCT");

            migrationBuilder.DropTable(
                name: "SALE");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
