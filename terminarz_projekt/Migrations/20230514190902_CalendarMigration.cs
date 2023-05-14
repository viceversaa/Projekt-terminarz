using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace terminarz_projekt.Migrations
{
    public partial class CalendarMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarModel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DaysInMonth = table.Column<int>(type: "int", nullable: false),
                    typ_zajec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pracownik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dzien_data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    godzina_od = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    godzina_do = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarModel", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarModel");
        }
    }
}
