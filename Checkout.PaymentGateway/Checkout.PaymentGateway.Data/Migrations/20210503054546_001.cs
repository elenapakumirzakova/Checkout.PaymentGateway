using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.PaymentGateway.Data.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "client",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "merchant",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    merchantUniqueToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_merchant", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "request",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    merchantUniqueToken = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    cardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cvc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "card",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    cardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cvc = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    expirationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    clientId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_card_client_clientId",
                        column: x => x.clientId,
                        principalSchema: "dbo",
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    merchantId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    cardId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    bankOperationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paymentStatus = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    timeStamp = table.Column<DateTime>(type: "datetime2(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_payment_card_cardId",
                        column: x => x.cardId,
                        principalSchema: "dbo",
                        principalTable: "card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_payment_merchant_merchantId",
                        column: x => x.merchantId,
                        principalSchema: "dbo",
                        principalTable: "merchant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "client",
                columns: new[] { "id", "firstName", "lastName" },
                values: new object[] { new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"), "John", "Doe" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "merchant",
                columns: new[] { "id", "merchantUniqueToken", "name" },
                values: new object[] { new Guid("745be9cf-7c3f-4c51-b91b-2c8c037c67de"), new Guid("8f9e6b51-3eb3-4025-a96f-4343a591bf1f"), "Merchant" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "card",
                columns: new[] { "id", "cardNumber", "clientId", "cvc", "expirationDate" },
                values: new object[] { new Guid("42d6f985-096a-4697-8d93-6e28e5a822e9"), "BpxgZtx/WIo7W6xK9MvmWrely+IMe1vBYDunBVGJLdc=", new Guid("5c032979-8fb2-443a-8667-6b29cf02ecc2"), "111", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "payment",
                columns: new[] { "id", "amount", "bankOperationId", "cardId", "merchantId", "paymentStatus", "timeStamp" },
                values: new object[] { new Guid("7c0d6f2b-3899-485d-ad6b-6c22d6ee6750"), 22.60m, new Guid("fbc9d5ea-239a-48f3-a6bc-8adda6507e04"), new Guid("42d6f985-096a-4697-8d93-6e28e5a822e9"), new Guid("745be9cf-7c3f-4c51-b91b-2c8c037c67de"), (byte)2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_card_clientId",
                schema: "dbo",
                table: "card",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_cardId",
                schema: "dbo",
                table: "payment",
                column: "cardId");

            migrationBuilder.CreateIndex(
                name: "IX_payment_merchantId",
                schema: "dbo",
                table: "payment",
                column: "merchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "request",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "card",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "merchant",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "client",
                schema: "dbo");
        }
    }
}
