using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class guidEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_customer_tb_m_account_account_guid",
                table: "tb_m_customer");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_account_account_guid",
                table: "tb_m_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_employee_account_guid",
                table: "tb_m_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_customer_account_guid",
                table: "tb_m_customer");

            migrationBuilder.AlterColumn<Guid>(
                name: "account_guid",
                table: "tb_m_employee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "account_guid",
                table: "tb_m_customer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_account_guid",
                table: "tb_m_employee",
                column: "account_guid",
                unique: true,
                filter: "[account_guid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_customer_account_guid",
                table: "tb_m_customer",
                column: "account_guid",
                unique: true,
                filter: "[account_guid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_customer_tb_m_account_account_guid",
                table: "tb_m_customer",
                column: "account_guid",
                principalTable: "tb_m_account",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_account_account_guid",
                table: "tb_m_employee",
                column: "account_guid",
                principalTable: "tb_m_account",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_customer_tb_m_account_account_guid",
                table: "tb_m_customer");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_employee_tb_m_account_account_guid",
                table: "tb_m_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_employee_account_guid",
                table: "tb_m_employee");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_customer_account_guid",
                table: "tb_m_customer");

            migrationBuilder.AlterColumn<Guid>(
                name: "account_guid",
                table: "tb_m_employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "account_guid",
                table: "tb_m_customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_account_guid",
                table: "tb_m_employee",
                column: "account_guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_customer_account_guid",
                table: "tb_m_customer",
                column: "account_guid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_customer_tb_m_account_account_guid",
                table: "tb_m_customer",
                column: "account_guid",
                principalTable: "tb_m_account",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_employee_tb_m_account_account_guid",
                table: "tb_m_employee",
                column: "account_guid",
                principalTable: "tb_m_account",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
