using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Foundation;
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
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        private DeployPackageCommandOptions Options { get; set; }
        public DeployPackageCommandStep(DeployerContext deployerContext, DeployStep step, DeployPipeContext deployData, JobBinder jobBinder) : base(deployData)
        {
            _deployerContext = deployerContext;
            _jobBinder = jobBinder;
            Options = jobBinder.CreateOptionsFromInputProperties<DeployPackageCommandOptions>(step.InputProperties);
        }

        protected override void ProcessVariables()
        {
            ReplaceVariables(Options.GetType(), Options);
        }

        private string deployDirectory;

        protected override string Body()
        {

            var target = _deployerContext.Targets.FirstOrDefault(x => x.Id == DeployPipeContext.TargetId);



            var version = Directory.GetDirectories(PathHelper.GetArtifactPath(Options.PackageName)).OrderByDescending(x => x).FirstOrDefault();

            var artifactPath = PathHelper.GetArtifactVersionPath(Options.PackageName, version);

            var fileName = Path.GetFileName(artifactPath);


            deployDirectory = $"/var/deployerBins/{Options.PackageName}";


            var sshClient = target.SshPort.HasValue ? new SshClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new SshClient(target.HostName, target.SshUser, target.SshPassword);
            var scpClient = target.SshPort.HasValue ? new ScpClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new ScpClient(target.HostName, target.SshUser, target.SshPassword);

            sshClient.Connect();
            scpClient.Connect();

            RunSSHCommand(sshClient, "mkdir /var/deployerBins");
            RunSSHCommand(sshClient, $"rm -rf {deployDirectory}");
            RunSSHCommand(sshClient, $"mkdir {deployDirectory}");

            using (var s = File.Open(artifactPath, FileMode.Open))
            {
                RunScp(scpClient, s, $"{deployDirectory}/{fileName}");
            }

            RunSSHCommand(sshClient, $"cd {deployDirectory} && unzip {fileName}");
            RunSSHCommand(sshClient, $"rm -f {deployDirectory}/{fileName}");

            scpClient.Dispose();
            sshClient.Dispose();

            return Log;
        }

        protected override void SetOutputVariables()
        {
            DeployPipeContext.ContextVariables["DeployDirectory"] = deployDirectory;
        }
    }
}
