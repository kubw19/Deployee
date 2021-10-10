using Deployer.Domain;
using System.Collections.Generic;

namespace Deployer.Repositories.DeploySteps
{
    public interface IDeployStepsRepository : IRepositoryBase<DeployStep>
    {
        List<DeployStep> GetStepsByProjectId(int projectId);
        void RemoveInputPropertiesForDeployStep(int deployStepId);
    }
}