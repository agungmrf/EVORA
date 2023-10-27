using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class locationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_location_tb_m_sub_district_sub_district_guid",
                table: "tb_m_location");

            migrationBuilder.DropTable(
                name: "tb_m_sub_district");

            migrationBuilder.DropTable(
                name: "tb_m_district");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_location_sub_district_guid",
                table: "tb_m_location");

            migrationBuilder.DropColumn(
                name: "sub_district_guid",
                table: "tb_m_location");

            migrationBuilder.AddColumn<Guid>(
                name: "city_guid",
                table: "tb_m_location",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "district",
                table: "tb_m_location",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sub_district",
                table: "tb_m_location",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_location_city_guid",
                table: "tb_m_location",
                column: "city_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_location_tb_m_city_city_guid",
                table: "tb_m_location",
                column: "city_guid",
                principalTable: "tb_m_city",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_location_tb_m_city_city_guid",
                table: "tb_m_location");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_location_city_guid",
                table: "tb_m_location");

            migrationBuilder.DropColumn(
                name: "city_guid",
                table: "tb_m_location");

            migrationBuilder.DropColumn(
                name: "district",
                table: "tb_m_location");

            migrationBuilder.DropColumn(
                name: "sub_district",
                table: "tb_m_location");

            migrationBuilder.AddColumn<Guid>(
                name: "sub_district_guid",
                table: "tb_m_location",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "tb_m_district",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    city_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_district", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_district_tb_m_city_city_guid",
                        column: x => x.city_guid,
                        principalTable: "tb_m_city",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_sub_district",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    district_guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_sub_district", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_sub_district_tb_m_district_district_guid",
                        column: x => x.district_guid,
                        principalTable: "tb_m_district",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_location_sub_district_guid",
                table: "tb_m_location",
                column: "sub_district_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_district_city_guid",
                table: "tb_m_district",
                column: "city_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sub_district_district_guid",
                table: "tb_m_sub_district",
                column: "district_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_location_tb_m_sub_district_sub_district_guid",
                table: "tb_m_location",
                column: "sub_district_guid",
                principalTable: "tb_m_sub_district",
                principalColumn: "guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
