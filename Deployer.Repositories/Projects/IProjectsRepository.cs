using Deployer.DatabaseModel;
using Deployer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.Projects
{
    public interface IProjectsRepository : IRepositoryBase<Project>
    {
        Project GetProjectByReleaseId(int releaseId);
    }
}
