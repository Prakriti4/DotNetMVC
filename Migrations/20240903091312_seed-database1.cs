using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewModelTableFormation.Migrations
{
    public partial class seeddatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "SecurityStamp" },
                values: new object[] { "16768f71-c31c-4ef1-b080-3e8f34ce5028", "admin1@gmail.com", true, "da70f39a-f8e2-412e-bcb4-df6729a59015" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "SecurityStamp" },
                values: new object[] { "60fcc19c-3d57-486b-a6f7-43a2127703ee", "admin@gmail.com", false, "f3d61da2-5c35-4bd3-9f22-dae11ae15d90" });
        }
    }
}
