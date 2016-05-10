using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Rental2.Models;

namespace Rental2.Migrations.Rental
{
    [DbContext(typeof(RentalContext))]
    [Migration("20160509165401_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rental2.Models.Document", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Rental2.Models.Payment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateReceived");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("PaymentAmount");

                    b.Property<int>("YearlyRentalID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Rental2.Models.Property", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AcceptsCats");

                    b.Property<bool>("AcceptsDogs");

                    b.Property<string>("Address");

                    b.Property<double>("Bathrooms");

                    b.Property<double>("Bedrooms");

                    b.Property<string>("City");

                    b.Property<double>("Rent");

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Rental2.Models.Tenant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalTenants");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ForwardingAddress")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Rental2.Models.YearlyRental", b =>
                {
                    b.Property<int>("ID");

                    b.Property<DateTime?>("EndDate");

                    b.Property<int>("PropertyID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TenantID");

                    b.HasKey("ID");
                });

            modelBuilder.Entity("Rental2.Models.Payment", b =>
                {
                    b.HasOne("Rental2.Models.YearlyRental")
                        .WithMany()
                        .HasForeignKey("YearlyRentalID");
                });

            modelBuilder.Entity("Rental2.Models.YearlyRental", b =>
                {
                    b.HasOne("Rental2.Models.Property")
                        .WithMany()
                        .HasForeignKey("PropertyID");

                    b.HasOne("Rental2.Models.Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");
                });
        }
    }
}
