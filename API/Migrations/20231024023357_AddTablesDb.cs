using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddTablesDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_customer",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_customer", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_disctrict",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_disctrict", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    nik = table.Column<string>(type: "nchar(12)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_package_event",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_package_event", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_province",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_province", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_sub_disctrict",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DisctrictGuid = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_sub_disctrict", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_sub_disctrict_tb_m_disctrict_DisctrictGuid",
                        column: x => x.DisctrictGuid,
                        principalTable: "tb_m_disctrict",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false),
                    is_used = table.Column<bool>(type: "bit", nullable: false),
                    expired_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_customer_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_customer",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_guid",
                        column: x => x.guid,
                        principalTable: "tb_m_employee",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_city",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    province_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    disctrict_guid = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_city", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_city_tb_m_disctrict_disctrict_guid",
                        column: x => x.disctrict_guid,
                        principalTable: "tb_m_disctrict",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_city_tb_m_province_province_guid",
                        column: x => x.province_guid,
                        principalTable: "tb_m_province",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account_roles",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    role_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    account_guid = table.Column<string>(type: "nvarchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account_roles", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_account_account_guid",
                        column: x => x.account_guid,
                        principalTable: "tb_m_account",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_role_role_guid",
                        column: x => x.role_guid,
                        principalTable: "tb_m_role",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_location",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    city_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_location", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_location_tb_m_city_city_guid",
                        column: x => x.city_guid,
                        principalTable: "tb_m_city",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_transaction_event",
                columns: table => new
                {
                    guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    customer_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    package_event_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    location_guid = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    invoice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    event_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_transaction_event", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_event_tb_m_customer_customer_guid",
                        column: x => x.customer_guid,
                        principalTable: "tb_m_customer",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_event_tb_m_location_location_guid",
                        column: x => x.location_guid,
                        principalTable: "tb_m_location",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_transaction_event_tb_m_package_event_package_event_guid",
                        column: x => x.package_event_guid,
                        principalTable: "tb_m_package_event",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_account_guid",
                table: "tb_m_account_roles",
                column: "account_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_role_guid",
                table: "tb_m_account_roles",
                column: "role_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_city_disctrict_guid",
                table: "tb_m_city",
                column: "disctrict_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_city_province_guid",
                table: "tb_m_city",
                column: "province_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_customer_email",
                table: "tb_m_customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_customer_phone_number",
                table: "tb_m_customer",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_email",
                table: "tb_m_employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_nik",
                table: "tb_m_employee",
                column: "nik",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_phone_number",
                table: "tb_m_employee",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_location_city_guid",
                table: "tb_m_location",
                column: "city_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sub_disctrict_DisctrictGuid",
                table: "tb_m_sub_disctrict",
                column: "DisctrictGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_event_customer_guid",
                table: "tb_m_transaction_event",
                column: "customer_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_event_location_guid",
                table: "tb_m_transaction_event",
                column: "location_guid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_transaction_event_package_event_guid",
                table: "tb_m_transaction_event",
                column: "package_event_guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account_roles");

            migrationBuilder.DropTable(
                name: "tb_m_sub_disctrict");

            migrationBuilder.DropTable(
                name: "tb_m_transaction_event");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_location");

            migrationBuilder.DropTable(
                name: "tb_m_package_event");

            migrationBuilder.DropTable(
                name: "tb_m_customer");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_city");

            migrationBuilder.DropTable(
                name: "tb_m_disctrict");

            migrationBuilder.DropTable(
                name: "tb_m_province");
        }
    }
}
