using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class FixedLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_city_tb_m_district_district_guid",
                table: "tb_m_city");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_location_tb_m_city_city_guid",
                table: "tb_m_location");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_city_district_guid",
                table: "tb_m_city");

            migrationBuilder.DropColumn(
                name: "district_guid",
                table: "tb_m_city");

            migrationBuilder.RenameColumn(
                name: "city_guid",
                table: "tb_m_location",
                newName: "sub_district_guid");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_location_city_guid",
                table: "tb_m_location",
                newName: "IX_tb_m_location_sub_district_guid");

            migrationBuilder.RenameColumn(
                name: "sub_district_guid",
                table: "tb_m_district",
                newName: "city_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_district_city_guid",
                table: "tb_m_district",
                column: "city_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_district_tb_m_city_city_guid",
                table: "tb_m_district",
                column: "city_guid",
                principalTable: "tb_m_city",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_location_tb_m_sub_district_sub_district_guid",
                table: "tb_m_location",
                column: "sub_district_guid",
                principalTable: "tb_m_sub_district",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_district_tb_m_city_city_guid",
                table: "tb_m_district");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_location_tb_m_sub_district_sub_district_guid",
                table: "tb_m_location");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_district_city_guid",
                table: "tb_m_district");

            migrationBuilder.RenameColumn(
                name: "sub_district_guid",
                table: "tb_m_location",
                newName: "city_guid");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_location_sub_district_guid",
                table: "tb_m_location",
                newName: "IX_tb_m_location_city_guid");

            migrationBuilder.RenameColumn(
                name: "city_guid",
                table: "tb_m_district",
                newName: "sub_district_guid");

            migrationBuilder.AddColumn<Guid>(
                name: "district_guid",
                table: "tb_m_city",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_city_district_guid",
                table: "tb_m_city",
                column: "district_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_city_tb_m_district_district_guid",
                table: "tb_m_city",
                column: "district_guid",
                principalTable: "tb_m_district",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_location_tb_m_city_city_guid",
                table: "tb_m_location",
                column: "city_guid",
                principalTable: "tb_m_city",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
