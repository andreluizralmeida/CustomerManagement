namespace CustomerManagement.Infrastructure.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
public partial class CreateTableCustomer : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Customers_FirstName",
            table: "Customers",
            column: "FirstName"
            );

        migrationBuilder.CreateIndex(
          name: "IX_Customers_Surname",
          table: "Customers",
          column: "Surname"
          );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(name: "IX_Customers_FirstName");
        migrationBuilder.DropIndex(name: "IX_Customers_Surname");
        migrationBuilder.DropTable(name: "Customers");
    }
}