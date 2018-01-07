﻿// <auto-generated />
using Differences.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Differences.DataAccess.Migrations
{
    [DbContext(typeof(DifferencesDbContext))]
    [Migration("20180107081853_AddMicrosoftIdToUser")]
    partial class AddMicrosoftIdToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Differences.Interaction.EntityModels.Answer", b =>
                {
                    b.Property<int>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("Content")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<Guid>("OwnerId");

                    b.Property<int?>("ParentReplyId");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentReplyId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.LikeRecord", b =>
                {
                    b.Property<int>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnswerId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<int>("QuestionId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LikeRecords");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.Question", b =>
                {
                    b.Property<int>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(400);

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<Guid>("OwnerId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<string>("LinkedInId")
                        .HasMaxLength(50);

                    b.Property<string>("MicrosoftId")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserContributionLog", b =>
                {
                    b.Property<int>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContributeTypeId");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<int?>("SubjectId");

                    b.Property<Guid>("UserId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserContributionLogs");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserReputationLog", b =>
                {
                    b.Property<int>("Id")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<int>("ReputationTypeId");

                    b.Property<int?>("SubjectId");

                    b.Property<Guid>("UserId");

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserReputationLogs");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserScore", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<int>("ContributeValue");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreatedBy");

                    b.Property<DateTime?>("LastUpdateTime");

                    b.Property<Guid?>("LastUpdatedBy");

                    b.Property<decimal>("ReputationValue");

                    b.HasKey("Id");

                    b.ToTable("UserScores");
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.Answer", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Differences.Interaction.EntityModels.Answer")
                        .WithMany("SubAnswers")
                        .HasForeignKey("ParentReplyId");

                    b.HasOne("Differences.Interaction.EntityModels.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.LikeRecord", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.Question", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserContributionLog", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Differences.Interaction.EntityModels.UserScore")
                        .WithMany("ContributionLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserReputationLog", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Differences.Interaction.EntityModels.UserScore")
                        .WithMany("ReputationLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Differences.Interaction.EntityModels.UserScore", b =>
                {
                    b.HasOne("Differences.Interaction.EntityModels.User", "User")
                        .WithOne("UserScores")
                        .HasForeignKey("Differences.Interaction.EntityModels.UserScore", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
