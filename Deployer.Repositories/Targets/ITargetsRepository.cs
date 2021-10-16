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
    public interface ITargetsRepository : IRepositoryBase<Target>
    {
        List<Target> GetTargetsForRoleId(int targetRoleId);
        TargetRole GetTargetRoleById(int targetRoleId);
        List<TargetRole> GetAllTargetRoles();
    }
}
