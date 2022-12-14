// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisProject.Infrastructure.Persistence;

#nullable disable

namespace ThesisProject.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221217231334_UpdateDecimalColumnsInDatabase")]
    partial class UpdateDecimalColumnsInDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CalculatedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OrderStatusType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderStatusType");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderStatusModel", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "None"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Placed"
                        });
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.ProductModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.WarehouseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.WarehouseProductModel", b =>
                {
                    b.Property<Guid>("WarehouseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("WarehouseProducts");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderItemModel", b =>
                {
                    b.HasOne("ThesisProject.Infrastructure.Persistence.Models.OrderModel", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThesisProject.Infrastructure.Persistence.Models.ProductModel", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderModel", b =>
                {
                    b.HasOne("ThesisProject.Infrastructure.Persistence.Models.OrderStatusModel", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusType")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.WarehouseProductModel", b =>
                {
                    b.HasOne("ThesisProject.Infrastructure.Persistence.Models.ProductModel", "Product")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("ThesisProject.Infrastructure.Persistence.Models.WarehouseModel", "Warehouse")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderModel", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.OrderStatusModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.ProductModel", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("WarehouseProducts");
                });

            modelBuilder.Entity("ThesisProject.Infrastructure.Persistence.Models.WarehouseModel", b =>
                {
                    b.Navigation("WarehouseProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
