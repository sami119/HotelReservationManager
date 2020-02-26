using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelReservationManager.Data.Migrations
{
    public partial class RoomsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(nullable: false),
                    _Type = table.Column<int>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    PricePerBedForAdult = table.Column<decimal>(nullable: false),
                    PricePerBedForChild = table.Column<decimal>(nullable: false),
                    RoomNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
