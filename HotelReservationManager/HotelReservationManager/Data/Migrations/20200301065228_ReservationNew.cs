using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class ReservationNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "ClientsID",
                table: "Reservations",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReservationID",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
