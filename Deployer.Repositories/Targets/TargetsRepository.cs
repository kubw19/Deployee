using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Repositories.DeploySteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.Targets
{
    public class TargetsRepository : RepositoryBase<Target>, ITargetsRepository
    {
        public TargetsRepository(DeployerContext deployerContext) : base(deployerContext)
        {
        }



    }
}
