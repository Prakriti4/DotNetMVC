using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ViewModelTableFormation.Migrations
{
    public partial class gh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", 0, "5342961c-3b50-49f3-964e-4b98d229159e", "User", "admin@gmail.com", true, null, null, false, null, "ADMIN@GMAIL.COM", null, "AQAAAAEAACcQAAAAEBu7iqDtke9bF2GOYuU1jMkWOqG4X19tvEGVQVQmgmvHa0LN+1q14lWZxUJMysP4lA==", null, false, "94827c78-8c34-4256-af3d-60e5a951411b", false, "admin@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "ae1a593f-70f2-40b1-871c-3ca4f96f9662", "User", "admin@gmail.com", true, null, null, false, null, "ADMIN@GMAIL.COM", null, "AQAAAAEAACcQAAAAEHIP0y/9WHH/E6jJfUcOSjKL4mkyA2K1mKt/LBVwILytAZeMUCHjdUfWGo+mflxFtg==", null, false, "870e5039-5126-48e7-a029-3f8d12e386d2", false, "admin@gmail.com" });
        }
    }
}
