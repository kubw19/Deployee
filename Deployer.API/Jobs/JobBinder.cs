using Deployer.API.Jobs.Commands;
using Deployer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Deployer.API.Jobs.Commands.DeployPackageCommand;

namespace Deployer.API.Jobs
{
    public class JobBinder
    {
        private Dictionary<DeployStepType, Type> Bindings { get; set; } = new Dictionary<DeployStepType, Type>();
        private Dictionary<DeployStepType, Type> OptionsBindings { get; set; } = new Dictionary<DeployStepType, Type>();

        public JobBinder()
        {
            Bind<DeployPackageCommand, DeployPackageCommandOptions>(DeployStepType.DeployPackageCommand);
            Bind<RunCommand, RunCommandOptions>(DeployStepType.RunCommand);
            Bind<RunService, RunServiceOptions>(DeployStepType.RunService);
        }

        private void Bind<Command,Options>(DeployStepType type)
        {
            Bindings.Add(type, typeof(Command));
            OptionsBindings.Add(type, typeof(Options));
        }

        public CommandOptionsPair GetStepTypes(DeployStepType type)
        {
            return new CommandOptionsPair
            {
                Command = Bindings[type],
                Options = OptionsBindings[type]
            };
        }

        public T CreateOptionsFromInputProperties<T>(List<InputProperty> properties)
        {
            object instance = Activator.CreateInstance(typeof(T));

            foreach(var p in properties)
            {
                typeof(T).GetProperty(p.Name).SetValue(instance, p.Value);
            }

            return (T)instance;

        }
        
    }

    public class CommandOptionsPair
    {
        public Type Command { get; set; }
        public Type Options { get; set; }
    }
}
