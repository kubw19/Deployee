using Deployer.Foundation;
using Deployer.Logic.Targets.DTOS;
using System.Collections.Generic;

namespace Deployer.Logic.Targets
{
    public interface ITargetsLogic
    {
        void AddTarget(TargetCreateDto target);
        List<TargetReadDto> GetAllTargets();
        List<TargetRoleDto> GetTargetRoles();
        List<TargetReadDto> GetTargetsForRole(int targetRoleId);
        StatusResponse Remove(int id);
    }
}