using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Rental2.Migrations.Rental
{
    public partial class ppoopp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Payment_YearlyRental_YearlyRentalID", table: "Payment");
            migrationBuilder.DropForeignKey(name: "FK_YearlyRental_Property_PropertyID", table: "YearlyRental");
            migrationBuilder.DropForeignKey(name: "FK_YearlyRental_Tenant_TenantID", table: "YearlyRental");
            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentAmount",
                table: "Payment",
                nullable: false);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_YearlyRental_YearlyRentalID",
                table: "Payment",
                column: "YearlyRentalID",
                principalTable: "YearlyRental",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_YearlyRental_Property_PropertyID",
                table: "YearlyRental",
                column: "PropertyID",
                principalTable: "Property",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_YearlyRental_Tenant_TenantID",
                table: "YearlyRental",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Payment_YearlyRental_YearlyRentalID", table: "Payment");
            migrationBuilder.DropForeignKey(name: "FK_YearlyRental_Property_PropertyID", table: "YearlyRental");
            migrationBuilder.DropForeignKey(name: "FK_YearlyRental_Tenant_TenantID", table: "YearlyRental");
            migrationBuilder.AlterColumn<int>(
                name: "PaymentAmount",
                table: "Payment",
                nullable: false);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_YearlyRental_YearlyRentalID",
                table: "Payment",
                column: "YearlyRentalID",
                principalTable: "YearlyRental",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_YearlyRental_Property_PropertyID",
                table: "YearlyRental",
                column: "PropertyID",
                principalTable: "Property",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_YearlyRental_Tenant_TenantID",
                table: "YearlyRental",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
