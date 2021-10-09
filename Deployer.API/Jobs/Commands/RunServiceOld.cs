//using Newtonsoft.Json;
//using Renci.SshNet;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Deployer.API.Jobs.Commands
//{
//    [Obsolete]
//    public class RunServiceOld : StepBase
//    {
//        private readonly DeployerContext _deployerContext;
//        private RunServiceOldOptions Options { get; set; }
//        public RunServiceOld(DeployerContext deployerContext, DeployStepModel step, DeployPipeContext deployData) : base(deployData)
//        {
//            _deployerContext = deployerContext;
//            Options = JsonConvert.DeserializeObject<RunServiceOldOptions>(step.Options);
//        }

//        public override string DoJob()
//        {

//            var target = _deployerContext.Targets.FirstOrDefault(x => x.Id == DeployPipeContext.TargetId);

//            var artifactPath = PathHelper.GetArtifactPath(DeployPipeContext.ArtifactName, DeployPipeContext.Version);

//            var fileName = Path.GetFileName(artifactPath);

//            var serviceTemplate = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "templateService.service"));

//            var serviceText = serviceTemplate.Replace("{{ArtifactName}}", DeployPipeContext.ArtifactName);

//            var serviceCommand = Options.ExecServiceCommand.Replace("{{Deployer.DeployDirectory}}", $"/var/deployerBins/{DeployPipeContext.ArtifactName}/");

//            serviceText = serviceText.Replace("{{ExecStart}}", serviceCommand);

//            var deployDirectory = $"/var/deployerBins/{DeployPipeContext.ArtifactName}";

//            var sshClient = target.SshPort.HasValue ? new SshClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new SshClient(target.HostName, target.SshUser, target.SshPassword);
//            var scpClient = target.SshPort.HasValue ? new ScpClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new ScpClient(target.HostName, target.SshUser, target.SshPassword);

//            sshClient.Connect();
//            scpClient.Connect();

//            RunSSHCommand(sshClient, "mkdir /var/deployerBins");
//            RunSSHCommand(sshClient, $"rm -rf {deployDirectory}");
//            RunSSHCommand(sshClient, $"mkdir {deployDirectory}");

//            using (var s = File.Open(artifactPath, FileMode.Open))
//            {
//                RunScp(scpClient, s, $"{deployDirectory}/{fileName}");
//            }

//            RunSSHCommand(sshClient, $"cd {deployDirectory} && unzip {fileName}");
//            RunSSHCommand(sshClient, $"rm -f {deployDirectory}/{fileName}");

//            RunSSHCommand(sshClient, $"systemctl stop {DeployPipeContext.ArtifactName} >/dev/null 2>&1");

//            RunScp(scpClient, GenerateStreamFromString(serviceText), $"/etc/systemd/system/{DeployPipeContext.ArtifactName}.service");

//            RunSSHCommand(sshClient, $"systemctl enable {DeployPipeContext.ArtifactName}");
//            RunSSHCommand(sshClient, $"systemctl start {DeployPipeContext.ArtifactName}");
//            RunSSHCommand(sshClient, $"systemctl daemon-reload");

//            scpClient.Dispose();
//            sshClient.Dispose();

//            return Log;
//        }


//        private Stream GenerateStreamFromString(string s)
//        {
//            MemoryStream stream = new MemoryStream();
//            StreamWriter writer = new StreamWriter(stream);
//            writer.Write(s);
//            writer.Flush();
//            stream.Position = 0;
//            return stream;
//        }
//    }




//    public class RunServiceOldOptions
//    {
//        public string ExecServiceCommand { get; set; } = "/usr/bin/dotnet \"{{Deployer.DeployDirectory}}/file.dll\"";
//    }
//}
