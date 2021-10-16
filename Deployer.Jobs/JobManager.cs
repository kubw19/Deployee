using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps;
using Deployer.Jobs.Steps.Options;
using Deployer.Repositories.Releases;
using Deployer.Repositories.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Jobs
{
    public class JobManager : IJobManager
    {
        private readonly JobBinder _jobBinder;
        private readonly ITargetsRepository _targetsRepository;
        private readonly IReleasesRepository _releasesRepository;

        public JobManager(JobBinder jobBinder, ITargetsRepository targetsRepository, IReleasesRepository releasesRepository)
        {
            this._jobBinder = jobBinder;
            _targetsRepository = targetsRepository;
            _releasesRepository = releasesRepository;
        }
        public string DoJob(DeployStep step, DeployPipeContext deployPipeContext)
        {
            var typesData = _jobBinder.GetStepTypes(step.Type);
            Type command = typesData.Command;
            var options = _jobBinder.CreateOptionsFromInputProperties(typesData.Options, step.InputProperties);
            var targets = deployPipeContext.GetTargets(options.TargetRoleId);
            string log = "";
            foreach (var target in targets)
            {

                if (!deployPipeContext.IsError)
                {
                    log += $"Deploy to target {target.Name} has started\n";
                    var job = (IStep)Activator.CreateInstance(command, step, deployPipeContext, options, target);
                    log += "\n" + job.DoJob() + "\n";
                    log += $"\nDeploy to target {target.Name} has finished\n";
                }

            }

            return log;
        }

        public StepInfoDto GetStepOptions(DeployStepType type)
        {
            var types = _jobBinder.GetStepTypes(type);

            var optionsInstance = Activator.CreateInstance(types.Options);


            var properties = types.Options.GetProperties()
                .Where(x => x.Name != nameof(IStepOptions.OutputVariables))
                .Select(x => new InputPropertyDto { Name = x.Name, Type = x.PropertyType.Name, Value = x.GetValue(optionsInstance)?.ToString(), SpecialType = SpecialFieldAttribute.GetSpecialType(x) }).ToList();


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

