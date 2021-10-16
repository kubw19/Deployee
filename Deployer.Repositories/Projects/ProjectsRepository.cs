using Deployer.DatabaseModel;
using Deployer.Domain;
using Microsoft.EntityFrameworkCore;
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

        public Project GetProjectByReleaseId(int releaseId)
        {
            return _deployerContext.Releases.Include(x => x.Project).ThenInclude(x => x.DeploySteps).ThenInclude(x => x.InputProperties)
                .SingleOrDefault(x => x.Id == releaseId).Project;
        }
    }
}
