using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSys.Persistence.Migrations
{
    public partial class VersionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                table: "Customers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Customers");
        }
    }
}
