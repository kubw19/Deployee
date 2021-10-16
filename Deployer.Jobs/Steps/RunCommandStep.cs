using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Domain.Targets;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps.Options;
using Renci.SshNet;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps
{
    public class RunCommandStep : StepBase
    {
        private readonly DeployPipeContext _deployPipeContext;
        private readonly JobBinder _jobBinder;

        private RunCommandOptions LocalOptions => Options as RunCommandOptions;
        public RunCommandStep(DeployStep step, DeployPipeContext deployPipeContext, OptionsBase options, TargetDto target) : base(deployPipeContext, target, step)
        {
            _deployPipeContext = deployPipeContext;
            Options = options;
        }


        protected override void ProcessVariables()
        {
            ReplaceVariables(Options.GetType(), Options);
        }


        protected override string Body()
        {


            RunSSHCommand(LocalOptions.Command);

            return Log;
        }

        protected override void SetOutputVariables()
        {

        }
    }
}
