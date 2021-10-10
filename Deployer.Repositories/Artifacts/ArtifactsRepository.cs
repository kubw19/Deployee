using Deployer.DatabaseModel;
using Deployer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.Artifacts
{
    public class ArtifactsRepository : RepositoryBase<Artifact>, IArtifactsRepository
    {
        public ArtifactsRepository(DeployerContext deployerContext) : base(deployerContext)
        {
        }

        public Artifact GetByArtifactName(string name)
        {
            return _deployerContext.Artifacts.SingleOrDefault(x => x.Name == name);
        }

        public List<Artifact> GetArtifactsForProject(int projectId)
        {
            return _deployerContext.Artifacts
                .Include(x => x.Versions)
                .Where(x => x.Projects.Any(y => y.Id == projectId))
                .ToList();
        }
    }
}
