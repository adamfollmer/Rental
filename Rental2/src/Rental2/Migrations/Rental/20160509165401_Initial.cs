using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace Rental2.Migrations.Rental
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptsCats = table.Column<bool>(nullable: false),
                    AcceptsDogs = table.Column<bool>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Bathrooms = table.Column<double>(nullable: false),
                    Bedrooms = table.Column<double>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Rent = table.Column<double>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdditionalTenants = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    ForwardingAddress = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "YearlyRental",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    PropertyID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearlyRental", x => x.ID);
                    table.ForeignKey(
                        name: "FK_YearlyRental_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YearlyRental_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentAmount = table.Column<int>(nullable: false),
                    YearlyRentalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payment_YearlyRental_YearlyRentalID",
                        column: x => x.YearlyRentalID,
                        principalTable: "YearlyRental",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Document");
            migrationBuilder.DropTable("Payment");
            migrationBuilder.DropTable("YearlyRental");
            migrationBuilder.DropTable("Property");
            migrationBuilder.DropTable("Tenant");
        }
    }
}
