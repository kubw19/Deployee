using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Domain.Targets;
using Deployer.Foundation;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps.Options;
using Renci.SshNet;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps
{
    public partial class DeployPackageCommandStep : StepBase
    {
        private readonly DeployPipeContext _deployContext;

        private DeployPackageCommandOptions LocalOptions => Options as DeployPackageCommandOptions;
        public DeployPackageCommandStep(DeployStep step, DeployPipeContext deployContext, OptionsBase options, TargetDto target) : base(deployContext, target, step)
        {
            _deployContext = deployContext;
            Options = options;
        }

        protected override void ProcessVariables()
        {
            ReplaceVariables(LocalOptions.GetType(), LocalOptions);
        }

        private string _deployDirectory;

        protected override string Body()
        {


            var artifact = _deployContext.Artifacts[LocalOptions.Artifact];

            _deployDirectory = $"/var/deployerBins/{artifact.Guid}";



            RunSSHCommand("mkdir -p /var/deployerBins");
            RunSSHCommand($"rm -rf {_deployDirectory}");
            RunSSHCommand($"mkdir {_deployDirectory}");

            using (var s = File.Open(artifact.Path, FileMode.Open))
            {
                RunScp(s, $"{_deployDirectory}/{artifact.FileName}");
            }

            RunSSHCommand($"cd {_deployDirectory} && unzip {artifact.FileName}");
            RunSSHCommand($"rm -f {_deployDirectory}/{artifact.FileName}");



            return Log;
        }

        protected override void SetOutputVariables()
        {
            DeployPipeContext.ContextVariables["DeployDirectory"] = _deployDirectory;
        }
    }
}
