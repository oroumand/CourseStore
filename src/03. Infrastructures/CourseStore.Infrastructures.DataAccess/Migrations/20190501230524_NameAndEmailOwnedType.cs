using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseStore.Infrastructures.DataAccess.Migrations
{
    public partial class NameAndEmailOwnedType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ExpirationDate", "PurchaseDate" },
                values: new object[] { new DateTime(2019, 5, 3, 3, 35, 23, 926, DateTimeKind.Local).AddTicks(6396), new DateTime(2019, 5, 1, 3, 35, 23, 928, DateTimeKind.Local).AddTicks(5295) });

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PurchaseDate",
                value: new DateTime(2019, 4, 22, 3, 35, 23, 928, DateTimeKind.Local).AddTicks(5870));

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PurchaseDate",
                value: new DateTime(2019, 5, 1, 3, 35, 23, 928, DateTimeKind.Local).AddTicks(5879));

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ExpirationDate", "PurchaseDate" },
                values: new object[] { new DateTime(2019, 5, 3, 3, 35, 23, 928, DateTimeKind.Local).AddTicks(5883), new DateTime(2019, 5, 1, 3, 35, 23, 928, DateTimeKind.Local).AddTicks(5885) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MoneySpent", "Status", "StatusExpirationDate" },
                values: new object[] { 1L, "mlotfi@gmail.com", "محمد", "لطفی", 200000m, 2, new DateTime(2019, 5, 17, 2, 0, 1, 383, DateTimeKind.Local).AddTicks(3375) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MoneySpent", "Status", "StatusExpirationDate" },
                values: new object[] { 2L, "a.Azhdari@gmail.com", "آرش", "اژدری", 20000m, 1, null });

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ExpirationDate", "PurchaseDate" },
                values: new object[] { new DateTime(2019, 5, 3, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(3746), new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4366) });

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "PurchaseDate",
                value: new DateTime(2019, 4, 22, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4884));

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "PurchaseDate",
                value: new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4893));

            migrationBuilder.UpdateData(
                table: "PurchasedCourses",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "ExpirationDate", "PurchaseDate" },
                values: new object[] { new DateTime(2019, 5, 3, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4897), new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4899) });
        }
    }
}
