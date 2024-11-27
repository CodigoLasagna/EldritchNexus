﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Helper.Migrations
{
    [DbContext(typeof(GitNexusDBContext))]
    [Migration("20241127145306_projectUserCreator")]
    partial class projectUserCreator
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("Domain.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminsIds")
                        .HasColumnType("longtext");

                    b.Property<string>("Hostname")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("OrganizationIconUrl")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("GitRepositoryPath")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("TeamId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminsIds")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("OrganizationId")
                        .HasColumnType("int");

                    b.Property<bool?>("State")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImageUrl")
                        .HasMaxLength(1200)
                        .HasColumnType("varchar(1200)");

                    b.Property<int?>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NodeOrganization", b =>
                {
                    b.Property<int>("NodesId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationsId")
                        .HasColumnType("int");

                    b.HasKey("NodesId", "OrganizationsId");

                    b.HasIndex("OrganizationsId");

                    b.ToTable("NodeOrganization");
                });

            modelBuilder.Entity("OrganizationUser", b =>
                {
                    b.Property<int>("OrganizationsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("OrganizationsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("OrganizationUser");
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.Property<int>("TeamsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("TeamsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("TeamUser");
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.HasOne("Domain.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Domain.Team", "Team")
                        .WithMany("Projects")
                        .HasForeignKey("TeamId");

                    b.Navigation("Creator");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.HasOne("Domain.Organization", "Organization")
                        .WithMany("Teams")
                        .HasForeignKey("OrganizationId");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("NodeOrganization", b =>
                {
                    b.HasOne("Domain.Node", null)
                        .WithMany()
                        .HasForeignKey("NodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrganizationUser", b =>
                {
                    b.HasOne("Domain.Organization", null)
                        .WithMany()
                        .HasForeignKey("OrganizationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamUser", b =>
                {
                    b.HasOne("Domain.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Organization", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}