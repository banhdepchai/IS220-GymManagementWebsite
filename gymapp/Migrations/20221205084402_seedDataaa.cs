using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gymapp.Migrations
{
    public partial class seedDataaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 4, "Ưu tiên xếp lớp", 3, 500000m, 2, "Đồng" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 5, "Được sử dụng phòng tắm, và cá tiện ích gói trên", 3, 1000000m, 3, "Bạc" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 6, "Có huấn luyện viên cá nhân, và cá tiện ích gói trên", 3, 30000000m, 4, "Vàng" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "MembershipId",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 1, "Ưu tiên xếp lớp", 3, 500000m, 2, "Đồng" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 2, "Được sử dụng phòng tắm, và cá tiện ích gói trên", 3, 1000000m, 3, "Bạc" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "MembershipId", "Bonus", "Duration", "Fee", "Hours", "Level" },
                values: new object[] { 3, "Có huấn luyện viên cá nhân, và cá tiện ích gói trên", 3, 30000000m, 4, "Vàng" });
        }
    }
}
