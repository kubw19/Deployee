using Deployer.DatabaseModel;
using Deployer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Repositories.DeploySteps
{
    public class DeployStepsRepository : RepositoryBase<DeployStep>, IDeployStepsRepository
    {

        public DeployStepsRepository(DeployerContext deployerContext) : base(deployerContext) { }


        public void RemoveInputPropertiesForDeployStep(int deployStepId)
        {
            _deployerContext.RemoveRange(_deployerContext.InputProperties.Where(x => x.DeployStepId == deployStepId));
        }

        public List<DeployStep> GetStepsByProjectId(int projectId)
        {
            return _deployerContext.DeploySteps
                .Include(x => x.InputProperties)
                .Where(x => x.ProjectId == projectId)
                .ToList();
        }

        public override DeployStep Get(int id)
        {
            return base.GetByIdQueryable(id)
                .Include(x => x.InputProperties)
                .SingleOrDefault();
        }


    }
}
