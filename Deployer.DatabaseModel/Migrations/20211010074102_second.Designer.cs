﻿// <auto-generated />
using System;
using Deployer.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Deployer.DatabaseModel.Migrations
{
    [DbContext(typeof(DeployerContext))]
    [Migration("20211010074102_second")]
    partial class second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("ArtifactProject", b =>
                {
                    b.Property<int>("ArtifactsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArtifactsId", "ProjectsId");

                    b.HasIndex("ProjectsId");

                    b.ToTable("ArtifactProject");
                });

            modelBuilder.Entity("Deployer.Domain.Artifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Artifacts");
                });

            modelBuilder.Entity("Deployer.Domain.ArtifactVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtifactId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ChannelId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.ToTable("ArtifactVersions");
                });

            modelBuilder.Entity("Deployer.Domain.DeployStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("DeploySteps");
                });

            modelBuilder.Entity("Deployer.Domain.InputProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeployStepId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeployStepId");

                    b.ToTable("InputProperties");
                });

            modelBuilder.Entity("Deployer.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Deployer.Domain.Release.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Releases", "schReleases");
                });

            modelBuilder.Entity("Deployer.Domain.Release.ReleaseArtifact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtifactId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArtifactId");

                    b.HasIndex("ReleaseId");

                    b.ToTable("ReleaseArtifacts", "schReleases");
                });

            modelBuilder.Entity("Deployer.Domain.Target", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("HostName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastVersion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("SshPassword")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SshPort")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SshUser")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Targets");
                });

            modelBuilder.Entity("ArtifactProject", b =>
                {
                    b.HasOne("Deployer.Domain.Artifact", null)
                        .WithMany()
                        .HasForeignKey("ArtifactsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Deployer.Domain.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Deployer.Domain.ArtifactVersion", b =>
                {
                    b.HasOne("Deployer.Domain.Artifact", "Artifact")
                        .WithMany("Versions")
                        .HasForeignKey("ArtifactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artifact");
                });

            modelBuilder.Entity("Deployer.Domain.DeployStep", b =>
                {
                    b.HasOne("Deployer.Domain.Project", "Project")
                        .WithMany("DeploySteps")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Deployer.Domain.InputProperty", b =>
                {
                    b.HasOne("Deployer.Domain.DeployStep", "DeployStep")
                        .WithMany("InputProperties")
                        .HasForeignKey("DeployStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeployStep");
                });

            modelBuilder.Entity("Deployer.Domain.Release.ReleaseArtifact", b =>
                {
                    b.HasOne("Deployer.Domain.ArtifactVersion", "Artifact")
                        .WithMany()
                        .HasForeignKey("ArtifactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Deployer.Domain.Release.Release", "Release")
                        .WithMany("Artifacts")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artifact");

                    b.Navigation("Release");
                });

            modelBuilder.Entity("Deployer.Domain.Artifact", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("Deployer.Domain.DeployStep", b =>
                {
                    b.Navigation("InputProperties");
                });

            modelBuilder.Entity("Deployer.Domain.Project", b =>
                {
                    b.Navigation("DeploySteps");
                });

            modelBuilder.Entity("Deployer.Domain.Release.Release", b =>
                {
                    b.Navigation("Artifacts");
                });
#pragma warning restore 612, 618
        }
    }
}
