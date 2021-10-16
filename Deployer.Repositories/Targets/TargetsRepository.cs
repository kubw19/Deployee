using Deployer.DatabaseModel;
using Deployer.Domain.Targets;
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

        public TargetRole GetTargetRoleById(int targetRoleId)
        {
            return _deployerContext.TargetRoles.SingleOrDefault(x => x.Id == targetRoleId);
        }

        public List<Target> GetTargetsForRoleId(int targetRoleId)
        {
            return _deployerContext.Targets
                .Where(x => x.TargetRoles.Any(x => x.Id == targetRoleId))
                .ToList();
        }

        public List<TargetRole> GetAllTargetRoles()
        {
            return _deployerContext.TargetRoles.ToList();
        }



    }
}
