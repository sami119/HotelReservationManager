using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class AddedReservationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationID",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAppointment",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDismissal",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EGN",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Familyname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservedRoomID = table.Column<int>(nullable: true),
                    UserMadeTheReservationId = table.Column<string>(nullable: true),
                    DateOfCheckIn = table.Column<DateTime>(nullable: false),
                    DateOfCheckOut = table.Column<DateTime>(nullable: false),
                    IncludeBreakfast = table.Column<bool>(nullable: false),
                    AllInclusive = table.Column<bool>(nullable: false),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservations_Rooms_ReservedRoomID",
                        column: x => x.ReservedRoomID,
                        principalTable: "Rooms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserMadeTheReservationId",
                        column: x => x.UserMadeTheReservationId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients",
                column: "ReservationID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservedRoomID",
                table: "Reservations",
                column: "ReservedRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserMadeTheReservationId",
                table: "Reservations",
                column: "UserMadeTheReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients",
                column: "ReservationID",
                principalTable: "Reservations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Reservations_ReservationID",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ReservationID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DateOfAppointment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfDismissal",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EGN",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Familyname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
