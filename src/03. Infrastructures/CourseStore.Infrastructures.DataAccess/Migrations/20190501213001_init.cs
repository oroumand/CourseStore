using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseStore.Infrastructures.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    LicensingModel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusExpirationDate = table.Column<DateTime>(nullable: true),
                    MoneySpent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchasedCourses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<long>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchasedCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasedCourses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "LicensingModel", "Name" },
                values: new object[,]
                {
                    { 1L, 1, "کارگاه DDD" },
                    { 2L, 2, "دوره آموزشی NoSQLهای پرکاربرد" },
                    { 3L, 2, "دوره آموزش ASP.NET Core 3.0 پیشرفته " }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "MoneySpent", "Status", "StatusExpirationDate" },
                values: new object[,]
                {
                    { 1L, "mlotfi@gmail.com", "محمد", "لطفی", 200000m, 2, new DateTime(2019, 5, 17, 2, 0, 1, 383, DateTimeKind.Local).AddTicks(3375) },
                    { 2L, "a.Azhdari@gmail.com", "آرش", "اژدری", 20000m, 1, null }
                });

            migrationBuilder.InsertData(
                table: "PurchasedCourses",
                columns: new[] { "Id", "CourseId", "CustomerId", "ExpirationDate", "Price", "PurchaseDate" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2019, 5, 3, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(3746), 20000m, new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4366) },
                    { 2L, 2L, 1L, null, 80000m, new DateTime(2019, 4, 22, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4884) },
                    { 3L, 3L, 1L, null, 100000m, new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4893) },
                    { 4L, 1L, 1L, new DateTime(2019, 5, 3, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4897), 20000m, new DateTime(2019, 5, 1, 2, 0, 1, 386, DateTimeKind.Local).AddTicks(4899) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CourseId",
                table: "PurchasedCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedCourses_CustomerId",
                table: "PurchasedCourses",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasedCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
