using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployeesNew.Migrations
{
    /// <inheritdoc />
    public partial class initialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Name" },
                values: new object[,]
                {
                    { new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
                    { new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Age", "CompanyId", "Name", "Position" },
                values: new object[,]
                {
                    { new Guid("8b831a47-e957-4beb-ba2b-18b5a1ffc610"), 35, new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9"), "Kane Miller", "Administrator" },
                    { new Guid("b70a7067-a271-41c8-b996-2722a3410278"), 26, new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"), "Sam Raiden", "Software developer" },
                    { new Guid("c9977417-2d8d-4b00-8c7a-134d8dcecf8f"), 30, new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"), "Jana McLeaf", "Software developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("8b831a47-e957-4beb-ba2b-18b5a1ffc610"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("b70a7067-a271-41c8-b996-2722a3410278"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeId",
                keyValue: new Guid("c9977417-2d8d-4b00-8c7a-134d8dcecf8f"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2"));
        }
    }
}
