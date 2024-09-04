using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewModelTableFormation.Migrations
{
    public partial class seeddatabas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f989573b-09d3-4b97-ab76-ca3496f30a9e", "admin@gmail.com", "AQAAAAEAACcQAAAAEJfytXjK4/s/GOmgQ6HKcEZRvO1nmCvafPLjGOY6aVIsi9PlGskFY9UuHb6I79+RhA==", "e9148a23-23d6-4137-ba7c-5d80849da3ea" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16768f71-c31c-4ef1-b080-3e8f34ce5028", "admin1@gmail.com", null, "da70f39a-f8e2-412e-bcb4-df6729a59015" });
        }
    }
}
