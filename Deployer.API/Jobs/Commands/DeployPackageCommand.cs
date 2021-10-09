using Deployer.API.Models;
using Newtonsoft.Json;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Deployer.API.Jobs.Commands
{
    public class DeployPackageCommand : StepBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        private DeployPackageCommandOptions Options { get; set; }
        public DeployPackageCommand(DeployerContext deployerContext, DeployStep step, DeployPipeContext deployData, JobBinder jobBinder) : base(deployData)
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



            var version = Directory.GetDirectories(PathHelper.GetArtifactPath(Options.PackageName)).OrderByDescending(x=>x).FirstOrDefault();

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

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private string EncodeName(string name)
        {
            char[] invalids = Path.GetInvalidFileNameChars();
            return new string(name.Select(c => invalids.Contains(c) ? '_' : c).ToArray());
        }

        public class DeployPackageCommandOptions : IJobOptions
        {
            public string PackageName { get; set; } = "";
            //public string Version { get; set; } = "";

            public IEnumerable<string> OutputVariables { get; } = new List<string>
            {
                "DeployDirectory"
            };
        }
    }
}
