using Deployer.API.Jobs.Commands;
using Deployer.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Jobs
{
    public class JobManager : IJobManager
    {
        private readonly DeployerContext deployerContext;
        private readonly JobBinder jobBinder;

        public JobManager(DeployerContext deployerContext, JobBinder jobBinder)
        {
            this.deployerContext = deployerContext;
            this.jobBinder = jobBinder;
        }
        public string DoJob(DeployStep step, DeployPipeContext context)
        {
            Type command = jobBinder.GetStepTypes(step.Type).Command;
            var job = (IStepJob)Activator.CreateInstance(command, deployerContext, step, context, jobBinder);
            return job.DoJob();
        }

        public StepInfo GetJobOptions(DeployStepType type)
        {
            var types = jobBinder.GetStepTypes(type);

            var optionsInstance = Activator.CreateInstance(types.Options);


            var properties = types.Options.GetProperties()
                .Where(x => x.Name != nameof(IJobOptions.OutputVariables))
                .Select(x => new InputProperty { Name = x.Name, Type = x.PropertyType.Name, Value = x.GetValue(optionsInstance)?.ToString() });


            return new StepInfo
            {
                InputProperties = properties.ToList(),
                OutputVariables = ((IJobOptions)Activator.CreateInstance(types.Options)).OutputVariables.ToList(),
                Type = type,
                TypeName = type.ToString()
            };
        }

        public List<StepDto> GetAvailableSteps()
        {
            return Enum.GetValues<DeployStepType>().Select(x => new StepDto
            {
                Name = x.ToString(),
                TypeId = (int)x
            }).ToList();
        }

    }



    public class StepDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
    }

    public class StepInfo
    {
        public DeployStepType Type { get; set; }
        public string TypeName { get; set; }
        public List<InputProperty> InputProperties { get; set; }
        public List<string> OutputVariables { get; set; }
    }
}

