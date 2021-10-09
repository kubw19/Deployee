using Deployer.API.Models;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Jobs.Commands
{
    public class RunCommand : StepBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        private RunCommandOptions Options { get; set; }
        public RunCommand(DeployerContext deployerContext, DeployStep step, DeployPipeContext deployPipeContext, JobBinder jobBinder) : base(deployPipeContext)
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




    public class RunCommandOptions : IJobOptions
    {
        public string Command { get; set; } = "echo hej";

        public IEnumerable<string> OutputVariables => new List<string>();
    }
}
