using Deployer.DatabaseModel;
using Deployer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.Projects
{
    public class ProjectsRepository : RepositoryBase<Project>, IProjectsRepository
    {
        public ProjectsRepository(DeployerContext deployerContext) : base(deployerContext)
        {
        }
    }
}
