using Deployer.Foundation;
using Deployer.Logic.Targets.DTOS;
using System.Collections.Generic;

namespace Deployer.Logic.Targets
{
    public interface ITargetsLogic
    {
        void AddTarget(TargetCreateDto target);
        List<TargetReadDto> GetAllTargets();
        StatusResponse Remove(int id);
    }
}