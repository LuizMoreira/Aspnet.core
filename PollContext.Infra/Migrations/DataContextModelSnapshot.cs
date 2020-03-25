﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PollContext.Infra.Contexts;

namespace PollContext.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PollContext.Domain.Entities.OptionPoll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Poll_Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Qty")
                        .HasColumnName("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Poll_Id");

                    b.ToTable("OptionsPoll");
                });

            modelBuilder.Entity("PollContext.Domain.Entities.Poll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Views")
                        .HasColumnName("View")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("PollContext.Domain.Entities.OptionPoll", b =>
                {
                    b.HasOne("PollContext.Domain.Entities.Poll", "Poll")
                        .WithMany("OptionsPoll")
                        .HasForeignKey("Poll_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("PollContext.Domain.ValueObjects.DescriptionVO", "Description", b1 =>
                        {
                            b1.Property<Guid>("OptionPollId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .HasColumnName("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("OptionPollId");

                            b1.ToTable("OptionsPoll");

                            b1.WithOwner()
                                .HasForeignKey("OptionPollId");
                        });
                });

            modelBuilder.Entity("PollContext.Domain.Entities.Poll", b =>
                {
                    b.OwnsOne("PollContext.Domain.ValueObjects.DescriptionVO", "Description", b1 =>
                        {
                            b1.Property<Guid>("PollId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .HasColumnName("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("PollId");

                            b1.ToTable("Polls");

                            b1.WithOwner()
                                .HasForeignKey("PollId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
