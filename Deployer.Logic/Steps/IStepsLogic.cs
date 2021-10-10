using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Jobs.DTOS;
using Deployer.Logic.Steps.DTOS;
using System.Collections.Generic;

namespace Deployer.Logic.Steps
{
    public interface IStepsLogic
    {
        StatusResponse<int> AddNewOrUpdateStep(InsertOrUpdateStepDto dto);
        List<StepSimpleDto> GetAllAvailableStepTemplates();
        StatusResponse<StepReadDto> GetStep(int stepId);
        List<StepReadDto> GetStepsForProject(int projectId);
        StepReadDto GetStepTemplate(DeployStepType type);
        StatusResponse RemoveStep(int stepId);
    }
}