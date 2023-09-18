using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyManagement.Data.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Tye",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reminders",
                newName: "TargetDate");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "Tye");

            migrationBuilder.RenameColumn(
                name: "TargetDate",
                table: "Reminders",
                newName: "Date");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WalletId = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_WalletId",
                table: "Budgets",
                column: "WalletId");
        }
    }
}
