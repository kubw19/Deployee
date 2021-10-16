using Deployer.Domain;
using Deployer.Domain.Release;
using Deployer.Domain.Targets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.DatabaseModel
{
    public class DeployerContext : DbContext
    {
        public DbSet<Target> Targets { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<ArtifactVersion> ArtifactVersions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<DeployStep> DeploySteps { get; set; }
        public DbSet<InputProperty> InputProperties { get; set; }

        public DbSet<Release> Releases { get; set; }
        public DbSet<ReleaseArtifact> ReleaseArtifacts { get; set; }
        public DbSet<TargetRole> TargetRoles { get; set; }
        public DbSet<ReleaseDeploy> ReleaseDeploys { get; set; }

        public DeployerContext(DbContextOptions<DeployerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artifact>().HasIndex(p => new { p.Name }).IsUnique(true);

            modelBuilder.Entity<TargetRole>().HasData(new TargetRole
            {
                Id = 1,
                Name = "Default role"
            });

            modelBuilder.Entity<Project>().HasData(new Project
            {
                Id = 1,
                Name = "Default project"
            });

        }
    }
}
