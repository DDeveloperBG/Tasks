﻿// <auto-generated />
using System;
using MUSACA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MUSACA.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211208101752_FixedProblemsWithStructure")]
    partial class FixedProblemsWithStructure
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MUSACA.Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CashierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nchar(12)")
                        .IsFixedLength(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Picture");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CashierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("IssuedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("MUSACA.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Order", b =>
                {
                    b.HasOne("MUSACA.Data.Models.User", "Cashier")
                        .WithMany("Orders")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MUSACA.Data.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MUSACA.Data.Models.Receipt", "Receipt")
                        .WithMany("Orders")
                        .HasForeignKey("ReceiptId");

                    b.Navigation("Cashier");

                    b.Navigation("Product");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Receipt", b =>
                {
                    b.HasOne("MUSACA.Data.Models.User", "Cashier")
                        .WithMany("Receipts")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Product", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MUSACA.Data.Models.Receipt", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MUSACA.Data.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}