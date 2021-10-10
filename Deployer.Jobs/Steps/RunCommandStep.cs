using Deployer.DatabaseModel;
using Deployer.Domain;
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
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        private RunCommandOptions Options { get; set; }
        public RunCommandStep(DeployerContext deployerContext, DeployStep step, DeployPipeContext deployPipeContext, JobBinder jobBinder) : base(deployPipeContext)
        {
            _deployerContext = deployerContext;
            _jobBinder = jobBinder;
            Options = jobBinder.CreateOptionsFromInputProperties<RunCommandOptions>(step.InputProperties);
        }


        protected override void ProcessVariables()
        {
            ReplaceVariables(Options.GetType(), Options);
        }


        protected override string Body()
        {

            var target = _deployerContext.Targets.FirstOrDefault(x => x.Id == DeployPipeContext.TargetId);

            var sshClient = target.SshPort.HasValue ? new SshClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new SshClient(target.HostName, target.SshUser, target.SshPassword);
            sshClient.Connect();


            RunSSHCommand(sshClient, Options.Command);


            return Log;
        }

        protected override void SetOutputVariables()
        {

        }
    }
}
