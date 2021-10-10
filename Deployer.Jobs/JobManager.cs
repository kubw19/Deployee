using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps;
using Deployer.Jobs.Steps.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Jobs
{
    public class JobManager : IJobManager
    {
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        public JobManager(DeployerContext deployerContext, JobBinder jobBinder)
        {
            this._deployerContext = deployerContext;
            this._jobBinder = jobBinder;
        }
        public string DoJob(DeployStep step, DeployPipeContext context)
        {
            Type command = _jobBinder.GetStepTypes(step.Type).Command;
            var job = (IStep)Activator.CreateInstance(command, _deployerContext, step, context, _jobBinder);
            return job.DoJob();
        }

        public StepInfoDto GetStepOptions(DeployStepType type)
        {
            var types = _jobBinder.GetStepTypes(type);

            var optionsInstance = Activator.CreateInstance(types.Options);


            var properties = types.Options.GetProperties()
                .Where(x => x.Name != nameof(IStepOptions.OutputVariables))
                .Select(x => new InputProperty { Name = x.Name, Type = x.PropertyType.Name, Value = x.GetValue(optionsInstance)?.ToString() });


            return new StepInfoDto
            {
                InputProperties = properties.ToList(),
                OutputVariables = ((IStepOptions)Activator.CreateInstance(types.Options)).OutputVariables.ToList(),
                Type = type,
                TypeName = type.ToString()
            };
        }

        public List<StepSimpleDto> GetAvailableSteps()
        {
            return Enum.GetValues<DeployStepType>().Select(x => new StepSimpleDto
            {
                Name = x.ToString(),
                TypeId = (int)x
            }).ToList();
        }

    }
}

