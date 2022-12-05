using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gymapp.Migrations
{
    public partial class updatePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SignupClasses",
                table: "SignupClasses");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "SignupClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignupClasses",
                table: "SignupClasses",
                columns: new[] { "ClassId", "UserId", "PaymentId" });

            migrationBuilder.CreateIndex(
                name: "IX_SignupClasses_PaymentId",
                table: "SignupClasses",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SignupClasses_Payments_PaymentId",
                table: "SignupClasses",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SignupClasses_Payments_PaymentId",
                table: "SignupClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SignupClasses",
                table: "SignupClasses");

            migrationBuilder.DropIndex(
                name: "IX_SignupClasses_PaymentId",
                table: "SignupClasses");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "SignupClasses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SignupClasses",
                table: "SignupClasses",
                columns: new[] { "ClassId", "UserId" });
        }
    }
}
