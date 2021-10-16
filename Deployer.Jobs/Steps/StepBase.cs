using Deployer.Domain;
using Deployer.Domain.Targets;
using Deployer.Jobs.DTOS;
using Deployer.Jobs.Steps.Options;
using Deployer.Repositories.Releases;
using Deployer.Repositories.Targets;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps
{
    public abstract class StepBase : IStep
    {
        private readonly DeployStep _deployStep;

        public string Log { get; set; } = "";
        public DeployPipeContext DeployPipeContext { get; set; }
        public TargetDto Target { get; }
        public OptionsBase Options { get; set; }

        public SshClient SshClient { get; set; }
        public ScpClient ScpClient { get; set; }

        public StepBase(DeployPipeContext deployPipeContext, TargetDto target, DeployStep deployStep)
        {
            DeployPipeContext = deployPipeContext;
            Target = target;
            _deployStep = deployStep;
        }


        public string DoJob()
        {
            var body = $"Step {_deployStep.Name} has started. \n";

            SshClient = Target.SshPort.HasValue ? new SshClient(Target.HostName, Target.SshPort.Value, Target.SshUser, Target.SshPassword) : new SshClient(Target.HostName, Target.SshUser, Target.SshPassword);
            ScpClient = Target.SshPort.HasValue ? new ScpClient(Target.HostName, Target.SshPort.Value, Target.SshUser, Target.SshPassword) : new ScpClient(Target.HostName, Target.SshUser, Target.SshPassword);

            try
            {
                SshClient.Connect();
                ScpClient.Connect();
            }
            catch
            {
                DeployPipeContext.IsError = true;
                return $"Could not connect to target {Target.Name}";
            }

            ProcessVariables();
            body += Body();
            SetOutputVariables();

            SshClient.Dispose();
            ScpClient.Dispose();
            body += $"Step {_deployStep.Name} has finished. \n";
            return body;
        }

        protected abstract void ProcessVariables();
        protected abstract string Body();
        protected abstract void SetOutputVariables();
        public void RunSSHCommand(string command)
        {
            string res = "";
            try
            {
                var cmd = SshClient.RunCommand(command);
                res = cmd.Result + " " + cmd.Error;
                if (cmd.ExitStatus < 0)
                {
                    DeployPipeContext.IsError = true;
                }
            }
            catch (Exception x)
            {
                res = x.Message;
                Log += res;
                DeployPipeContext.IsError = true;
                throw;
            }
            Log += res;
        }

        public void RunScp(Stream stream, string remotePath)
        {
            string res = "";
            try
            {
                ScpClient.Upload(stream, remotePath);
            }
            catch (Exception x)
            {
                res = x.Message;
                Log += res;
                DeployPipeContext.IsError = true;
                throw;
            }
            Log += res;
        }


        protected void ReplaceVariables(Type type, object options)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties().Where(x => x.PropertyType == typeof(string)))
            {
                var value = (string)propertyInfo.GetValue(options);

                foreach (var x in DeployPipeContext.ContextVariables)
                {
                    value = value.Replace("{{" + x.Key + "}}", x.Value);
                }
                propertyInfo.SetValue(options, value);
            }
        }
    }




    public class DeployPipeContext
    {

        public DeployPipeContext(int releaseId, ITargetsRepository targetsRepository, IReleasesRepository releasesRepository)
        {
            ReleaseId = releaseId;
            _targetsRepository = targetsRepository;
            _releasesRepository = releasesRepository;
        }

        public Dictionary<string, string> ContextVariables = new Dictionary<string, string>();
        private readonly ITargetsRepository _targetsRepository;
        private readonly IReleasesRepository _releasesRepository;

        public bool IsError { get; set; }

        public int ReleaseId { get; }

        public List<TargetDto> GetTargets(int roleId)
        {
            var models = _targetsRepository.GetTargetsForRoleId(roleId);
            var dtos = new List<TargetDto>();
            foreach (var m in models)
            {
                var s = new TargetDto
                {
                    HostName = m.HostName,
                    Name = m.Name,
                    SshPassword = m.SshPassword,
                    SshPort = m.SshPort,
                    SshUser = m.SshUser
                };
                dtos.Add(s);
            }
            return dtos;
        }

        public Dictionary<int, ArtifactVersion> Artifacts => _releasesRepository.GetArtifactsForRelease(ReleaseId).ToDictionary(x => x.ArtifactId, y => y);

    }
}
