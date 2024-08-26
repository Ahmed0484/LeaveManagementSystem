using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d9860ac-1d48-427e-bb26-72bcddbfb871", new DateOnly(1950, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEFHTu7r+e+7GlOZ/a/9sWils7KXReV5pQSyUimx6I/sUv6NDFCu1TlKk8ZFdg6COdg==", "d6e36d2f-7299-4384-ae06-fb224a370b54" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4bf194d-8579-4950-ad8c-09516700db4a", "AQAAAAIAAYagAAAAEKL5UT21rZRJBn5BZ1mIAoObn009EiRXjpULx4EuyWS+NumlWL6VWbX0Kh1baY5OxQ==", "06f1f0e2-d5a0-4da9-9fb4-d3ae40b6ee59" });
        }
    }
}
