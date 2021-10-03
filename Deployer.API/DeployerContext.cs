﻿using Deployer.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API
{
    public class DeployerContext : DbContext
    {
        public DbSet<Target> Targets { get; set; }
        public DbSet<Artifact> Artifacts { get; set; }
        public DeployerContext(DbContextOptions<DeployerContext> options) : base(options)
        {

        }
    }
}