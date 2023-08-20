﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using mcr.Data;

#nullable disable

namespace mcr.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230820174053_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("mcr.Data.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("mcr.Data.Models.Encoder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("clientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("clientId");

                    b.ToTable("Encoders");
                });

            modelBuilder.Entity("mcr.Data.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("EncoderId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("IntTxEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("IntTxStart")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TxEnd")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TxStart")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EncoderId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("mcr.Data.Models.Fee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("SignalId")
                        .HasColumnType("int");

                    b.Property<int>("SourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SignalId");

                    b.HasIndex("SourceId");

                    b.ToTable("Fees");
                });

            modelBuilder.Entity("mcr.Data.Models.Signal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Signals");
                });

            modelBuilder.Entity("mcr.Data.Models.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("mcr.Data.Models.Encoder", b =>
                {
                    b.HasOne("mcr.Data.Models.Client", "Client")
                        .WithMany("Encoders")
                        .HasForeignKey("clientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("mcr.Data.Models.Event", b =>
                {
                    b.HasOne("mcr.Data.Models.Encoder", "CC")
                        .WithMany()
                        .HasForeignKey("EncoderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CC");
                });

            modelBuilder.Entity("mcr.Data.Models.Fee", b =>
                {
                    b.HasOne("mcr.Data.Models.Event", "Event")
                        .WithMany("FeeList")
                        .HasForeignKey("EventId");

                    b.HasOne("mcr.Data.Models.Signal", "Signal")
                        .WithMany()
                        .HasForeignKey("SignalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("mcr.Data.Models.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Signal");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("mcr.Data.Models.Client", b =>
                {
                    b.Navigation("Encoders");
                });

            modelBuilder.Entity("mcr.Data.Models.Event", b =>
                {
                    b.Navigation("FeeList");
                });
#pragma warning restore 612, 618
        }
    }
}
