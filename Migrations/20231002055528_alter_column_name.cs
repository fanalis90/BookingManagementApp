using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingManagementApp.Migrations
{
    public partial class alter_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_id",
                table: "tb_tr_bookings");

            migrationBuilder.RenameColumn(
                name: "employee_id",
                table: "tb_tr_bookings",
                newName: "employee_guid");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_bookings_employee_id",
                table: "tb_tr_bookings",
                newName: "IX_tb_tr_bookings_employee_guid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings",
                column: "employee_guid",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_guid",
                table: "tb_tr_bookings");

            migrationBuilder.RenameColumn(
                name: "employee_guid",
                table: "tb_tr_bookings",
                newName: "employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_bookings_employee_guid",
                table: "tb_tr_bookings",
                newName: "IX_tb_tr_bookings_employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_bookings_tb_m_employees_employee_id",
                table: "tb_tr_bookings",
                column: "employee_id",
                principalTable: "tb_m_employees",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
