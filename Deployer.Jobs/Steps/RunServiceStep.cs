using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Jobs.Steps.Options;
using Renci.SshNet;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps
{
    public class RunServiceStep : StepBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly JobBinder _jobBinder;

        private RunServiceOptions Options { get; set; }
        public RunServiceStep(DeployerContext deployerContext, DeployStep step, DeployPipeContext deployData, JobBinder jobBinder) : base(deployData)
        {
            _deployerContext = deployerContext;
            _jobBinder = jobBinder;
            Options = jobBinder.CreateOptionsFromInputProperties<RunServiceOptions>(step.InputProperties);
        }

        protected override void ProcessVariables()
        {
            ReplaceVariables(Options.GetType(), Options);
        }

        protected override string Body()
        {

            var target = _deployerContext.Targets.FirstOrDefault(x => x.Id == DeployPipeContext.TargetId);

            var serviceTemplate = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "templateService.service"))
                    .Replace("{{Description}}", Options.Description)
                    .Replace("{{WorkDir}}", Options.WorkDir)
                    .Replace("{{ExecStart}}", Options.ExecServiceCommand)
                    .Replace("{{SafeName}}", EncodeName(Options.Name));


            var sshClient = target.SshPort.HasValue ? new SshClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new SshClient(target.HostName, target.SshUser, target.SshPassword);
            var scpClient = target.SshPort.HasValue ? new ScpClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new ScpClient(target.HostName, target.SshUser, target.SshPassword);

            sshClient.Connect();
            scpClient.Connect();



            RunSSHCommand(sshClient, $"systemctl stop {EncodeName(Options.Name)} >/dev/null 2>&1");

            RunScp(scpClient, GenerateStreamFromString(serviceTemplate), $"/etc/systemd/system/{EncodeName(Options.Name)}.service");

            RunSSHCommand(sshClient, $"systemctl enable {EncodeName(Options.Name)}");
            RunSSHCommand(sshClient, $"systemctl start {EncodeName(Options.Name)}");
            RunSSHCommand(sshClient, $"systemctl daemon-reload");

            scpClient.Dispose();
            sshClient.Dispose();

            return Log;
        }

        protected override void SetOutputVariables()
        {

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

    }
}
