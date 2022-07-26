﻿// <auto-generated />
using System;
using ArvatoLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArvatoLibrary.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ArvatoLibrary.DataModels.Currency", b =>
                {
                    b.Property<int?>("Sequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sequence");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.CurrencyDate", b =>
                {
                    b.Property<int?>("Sequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrencySequence")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Sequence");

                    b.HasIndex("CurrencySequence");

                    b.ToTable("CurrencyDate");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.CurrencyRate", b =>
                {
                    b.Property<int?>("Sequence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrencyDateSequence")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Sequence");

                    b.HasIndex("CurrencyDateSequence");

                    b.ToTable("CurrencyRate");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.CurrencyDate", b =>
                {
                    b.HasOne("ArvatoLibrary.DataModels.Currency", null)
                        .WithMany("Rates")
                        .HasForeignKey("CurrencySequence");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.CurrencyRate", b =>
                {
                    b.HasOne("ArvatoLibrary.DataModels.CurrencyDate", null)
                        .WithMany("Rates")
                        .HasForeignKey("CurrencyDateSequence");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.Currency", b =>
                {
                    b.Navigation("Rates");
                });

            modelBuilder.Entity("ArvatoLibrary.DataModels.CurrencyDate", b =>
                {
                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
