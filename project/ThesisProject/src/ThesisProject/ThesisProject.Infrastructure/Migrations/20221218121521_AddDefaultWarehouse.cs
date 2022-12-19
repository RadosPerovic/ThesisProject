using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisProject.Infrastructure.Migrations
{
    public partial class AddDefaultWarehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[] { new Guid("028e1da5-1c47-4a26-af40-720ece1f1697"), "Belgrade", "Warehouse1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: new Guid("028e1da5-1c47-4a26-af40-720ece1f1697"));
        }
    }
}
