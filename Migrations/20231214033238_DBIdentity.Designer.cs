﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjetWeb3Bourse.Data;

#nullable disable

namespace ProjetWeb3Bourse.Migrations
{
    [DbContext(typeof(BourseContext))]
    [Migration("20231214033238_DBIdentity")]
    partial class DBIdentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjetWeb3Bourse.Models.Bourse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("valeur")
                        .HasColumnType("float");

                    b.Property<double>("variation")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("Bourse");
                });

            modelBuilder.Entity("ProjetWeb3Bourse.Models.Evenement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("bourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("heure")
                        .HasColumnType("datetime2");

                    b.Property<double>("valeur")
                        .HasColumnType("float");

                    b.Property<double>("variation")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("bourseId");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("ProjetWeb3Bourse.Models.Evenement", b =>
                {
                    b.HasOne("ProjetWeb3Bourse.Models.Bourse", "bourse")
                        .WithMany("Evenements")
                        .HasForeignKey("bourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bourse");
                });

            modelBuilder.Entity("ProjetWeb3Bourse.Models.Bourse", b =>
                {
                    b.Navigation("Evenements");
                });
#pragma warning restore 612, 618
        }
    }
}
