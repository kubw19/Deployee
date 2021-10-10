using Deployer.Domain;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps;
using Deployer.Jobs.Steps.Options;
using System;
using System.Collections.Generic;

namespace Deployer.Jobs
{
    public class JobBinder
    {
        private Dictionary<DeployStepType, Type> Bindings { get; set; } = new Dictionary<DeployStepType, Type>();
        private Dictionary<DeployStepType, Type> OptionsBindings { get; set; } = new Dictionary<DeployStepType, Type>();

        public JobBinder()
        {
            Bind<DeployPackageCommandStep, DeployPackageCommandOptions>(DeployStepType.DeployPackageCommand);
            Bind<RunCommandStep, RunCommandOptions>(DeployStepType.RunCommand);
            Bind<RunServiceStep, RunServiceOptions>(DeployStepType.RunService);
        }

        private void Bind<Command, Options>(DeployStepType type)
        {
            Bindings.Add(type, typeof(Command));
            OptionsBindings.Add(type, typeof(Options));
        }

        public CommandOptionsPairDto GetStepTypes(DeployStepType type)
        {
            return new CommandOptionsPairDto
            {
                Command = Bindings[type],
                Options = OptionsBindings[type]
            };
        }

        public T CreateOptionsFromInputProperties<T>(List<InputProperty> properties)
        {
            object instance = Activator.CreateInstance(typeof(T));

            foreach (var p in properties)
            {
                typeof(T).GetProperty(p.Name).SetValue(instance, p.Value);
            }

            return (T)instance;

        }

    }
}
