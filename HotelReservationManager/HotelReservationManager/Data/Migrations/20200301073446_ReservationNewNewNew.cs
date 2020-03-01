using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class ReservationNewNewNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clients_ClientsID",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClientsID",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ClientsID",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Reservations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ClientsID",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientsID",
                table: "Reservations",
                column: "ClientsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clients_ClientsID",
                table: "Reservations",
                column: "ClientsID",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
