using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewModelTableFormation.Migrations
{
    public partial class @finally : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "ae1a593f-70f2-40b1-871c-3ca4f96f9662", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEHIP0y/9WHH/E6jJfUcOSjKL4mkyA2K1mKt/LBVwILytAZeMUCHjdUfWGo+mflxFtg==", "870e5039-5126-48e7-a029-3f8d12e386d2", "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f989573b-09d3-4b97-ab76-ca3496f30a9e", null, "AQAAAAEAACcQAAAAEJfytXjK4/s/GOmgQ6HKcEZRvO1nmCvafPLjGOY6aVIsi9PlGskFY9UuHb6I79+RhA==", "e9148a23-23d6-4137-ba7c-5d80849da3ea", "Admin" });
        }
    }
}
