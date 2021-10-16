using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Domain.Targets;
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
    public class RunServiceStep : StepBase
    {
        private readonly DeployPipeContext _deployContext;

        private RunServiceOptions LocalOptions => Options as RunServiceOptions;
        public RunServiceStep(DeployStep step, DeployPipeContext deployContext, OptionsBase options, TargetDto target) : base(deployContext, target, step)
        {
            _deployContext = deployContext;
            Options = options;
        }

        protected override void ProcessVariables()
        {
            ReplaceVariables(Options.GetType(), Options);
        }

        protected override string Body()
        {


            var serviceTemplate = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "templateService.service"))
                    .Replace("{{Description}}", LocalOptions.Description)
                    .Replace("{{WorkDir}}", LocalOptions.WorkDir)
                    .Replace("{{ExecStart}}", LocalOptions.ExecServiceCommand)
                    .Replace("{{SafeName}}", EncodeName(LocalOptions.Name));



            RunSSHCommand($"systemctl stop {EncodeName(LocalOptions.Name)} >/dev/null 2>&1");

            RunScp(GenerateStreamFromString(serviceTemplate), $"/etc/systemd/system/{EncodeName(LocalOptions.Name)}.service");

            RunSSHCommand($"systemctl enable {EncodeName(LocalOptions.Name)}");
            RunSSHCommand($"systemctl start {EncodeName(LocalOptions.Name)}");
            RunSSHCommand($"systemctl daemon-reload");

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
            name = name.Replace(" ", "");
            char[] invalids = Path.GetInvalidFileNameChars();
            return new string(name.Select(c => invalids.Contains(c) ? '_' : c).ToArray());
        }

    }
}
