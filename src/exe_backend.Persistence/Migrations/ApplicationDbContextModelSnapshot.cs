﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using exe_backend.Persistence;

#nullable disable

namespace exe_backend.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("exe_backend.Domain.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityCourses")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Chapter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityLectures")
                        .HasColumnType("int");

                    b.Property<double>("TotalDurationLectures")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityChapters")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LevelId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChapterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Level", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityCourses")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SubscriptionPackageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionPackageId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.SubscriptionPackage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpiredMonth")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPackages");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PublicAvatarId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicAvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Chapter", b =>
                {
                    b.HasOne("exe_backend.Domain.Models.Course", "Course")
                        .WithMany("Chapters")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Chapter_Course_CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Course", b =>
                {
                    b.HasOne("exe_backend.Domain.Models.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Category_Course_CourseId");

                    b.HasOne("exe_backend.Domain.Models.Level", "Level")
                        .WithMany("Courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Level_Course_CourseId");

                    b.OwnsOne("exe_backend.Domain.ValueObjects.Image", "Thumbnail", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PublicId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PublicUrl")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Level");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Lecture", b =>
                {
                    b.HasOne("exe_backend.Domain.Models.Chapter", "Chapter")
                        .WithMany("Lectures")
                        .HasForeignKey("ChapterId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Lecture_Chapter_ChapterId");

                    b.OwnsOne("exe_backend.Domain.ValueObjects.Image", "ImageLecture", b1 =>
                        {
                            b1.Property<Guid>("LectureId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("PublicId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PublicUrl")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("LectureId");

                            b1.ToTable("Lectures");

                            b1.WithOwner()
                                .HasForeignKey("LectureId");
                        });

                    b.OwnsOne("exe_backend.Domain.ValueObjects.Video", "VideoLecture", b1 =>
                        {
                            b1.Property<Guid>("LectureId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<double>("Duration")
                                .HasColumnType("float");

                            b1.Property<string>("PublicId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("LectureId");

                            b1.ToTable("Lectures");

                            b1.WithOwner()
                                .HasForeignKey("LectureId");
                        });

                    b.Navigation("Chapter");

                    b.Navigation("ImageLecture");

                    b.Navigation("VideoLecture");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Subscription", b =>
                {
                    b.HasOne("exe_backend.Domain.Models.SubscriptionPackage", "SubscriptionPackage")
                        .WithMany()
                        .HasForeignKey("SubscriptionPackageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("exe_backend.Domain.Models.User", "User")
                        .WithOne("Subscription")
                        .HasForeignKey("exe_backend.Domain.Models.Subscription", "UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("SubscriptionPackage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.User", b =>
                {
                    b.HasOne("exe_backend.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Chapter", b =>
                {
                    b.Navigation("Lectures");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Course", b =>
                {
                    b.Navigation("Chapters");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Level", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("exe_backend.Domain.Models.User", b =>
                {
                    b.Navigation("Subscription");
                });
#pragma warning restore 612, 618
        }
    }
}
