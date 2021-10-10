using Deployer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.Artifacts
{
    public interface IArtifactsRepository : IRepositoryBase<Artifact>
    {
        List<Artifact> GetArtifactsForProject(int projectId);
        Artifact GetByArtifactName(string name);
    }
}
