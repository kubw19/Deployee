using Deployer.API.Models;
using System.Collections.Generic;

namespace Deployer.API.Jobs
{
    public interface IJobManager
    {
        string DoJob(DeployStep step, DeployPipeContext context);
        StepInfo GetJobOptions(DeployStepType type);
        List<StepDto> GetAvailableSteps();
    }
}