using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyEmployeesNew.Migrations
{
    /// <inheritdoc />
    public partial class addedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e2d24d8-64b9-4e41-a67e-0dcb2b52ea00", "89621228-6ede-44c0-9134-46e8c47038d9", "Administrator", "ADMINISTRATOR" },
                    { "6c34a50c-e5eb-4f2e-888b-618a798bc03d", "8b07d8f7-bdad-4a38-9e08-c128e8e66a2e", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e2d24d8-64b9-4e41-a67e-0dcb2b52ea00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c34a50c-e5eb-4f2e-888b-618a798bc03d");
        }
    }
}
