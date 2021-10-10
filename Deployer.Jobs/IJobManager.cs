using Deployer.Domain;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps;
using System.Collections.Generic;

namespace Deployer.Jobs
{
    public interface IJobManager
    {
        string DoJob(DeployStep step, DeployPipeContext context);
        StepInfoDto GetStepOptions(DeployStepType type);
        List<StepSimpleDto> GetAvailableSteps();
    }
}