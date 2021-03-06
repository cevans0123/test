﻿// <auto-generated />
using Ideas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Storage.Internal;
using System;

namespace Ideas.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("Ideas.Models.Idea", b =>
                {
                    b.Property<int>("IdeaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Post")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("Ideas.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdeaId");

                    b.Property<int>("UserId");

                    b.HasKey("ParticipantId");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Ideas.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Ideas.Models.Idea", b =>
                {
                    b.HasOne("Ideas.Models.User", "User")
                        .WithMany("Ideas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ideas.Models.Participant", b =>
                {
                    b.HasOne("Ideas.Models.Idea", "Idea")
                        .WithMany("Participants")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ideas.Models.User", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
