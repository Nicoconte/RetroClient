﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetroClient.Data;

namespace RetroClient.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("RetroClient.Models.VideoGame", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DownloadUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Platform")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("VideoGames");
                });
#pragma warning restore 612, 618
        }
    }
}
