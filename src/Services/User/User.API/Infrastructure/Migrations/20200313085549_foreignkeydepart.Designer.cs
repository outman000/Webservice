﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using User.Infrastructure;

namespace User.API.Infrastructure.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20200313085549_foreignkeydepart")]
    partial class foreignkeydepart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("User.Domain.AggregatesModel.UserAggregates.Entitys.UserInformation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("_userStatusId")
                        .HasColumnName("userStatusId");

                    b.Property<DateTime?>("birthdate");

                    b.Property<DateTime?>("createtime");

                    b.Property<string>("description");

                    b.Property<string>("email");

                    b.Property<string>("gender");

                    b.Property<string>("mobileCall");

                    b.Property<string>("phoneCall");

                    b.Property<int?>("statusId");

                    b.Property<DateTime?>("updateTime");

                    b.Property<string>("userId");

                    b.Property<string>("userName");

                    b.Property<string>("userPwd");

                    b.HasKey("Id");

                    b.HasIndex("statusId");

                    b.ToTable("User_Information","PeopleManager");
                });

            modelBuilder.Entity("User.Domain.AggregatesModel.UserAggregates.Entitys.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");
                });

            modelBuilder.Entity("User.Domain.AggregatesModel.UserAggregates.Entitys.UserInformation", b =>
                {
                    b.HasOne("User.Domain.AggregatesModel.UserAggregates.Entitys.UserStatus", "status")
                        .WithMany()
                        .HasForeignKey("statusId");

                    b.OwnsOne("User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects.Address", "address", b1 =>
                        {
                            b1.Property<long>("UserInformationId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City");

                            b1.Property<string>("County");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.Property<string>("Zip");

                            b1.HasKey("UserInformationId");

                            b1.ToTable("User_Information","PeopleManager");

                            b1.HasOne("User.Domain.AggregatesModel.UserAggregates.Entitys.UserInformation")
                                .WithOne("address")
                                .HasForeignKey("User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects.Address", "UserInformationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects.CreateUserInfo", "createUserInfo", b1 =>
                        {
                            b1.Property<long>("UserInformationId")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("_createUserId");

                            b1.Property<string>("_createUserName");

                            b1.HasKey("UserInformationId");

                            b1.ToTable("User_Information","PeopleManager");

                            b1.HasOne("User.Domain.AggregatesModel.UserAggregates.Entitys.UserInformation")
                                .WithOne("createUserInfo")
                                .HasForeignKey("User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects.CreateUserInfo", "UserInformationId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
