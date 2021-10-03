using Microsoft.AspNetCore.Mvc;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeployController : ControllerBase
    {
        private readonly DeployerContext _deployerContext;

        public DeployController(DeployerContext deployerContext)
        {
            _deployerContext = deployerContext;
        }
        [HttpPost]
        public async Task<IActionResult> MakeArtifactDeploy(ArtifactDeployDto model)
        {
            var target = _deployerContext.Targets.FirstOrDefault(x => x.Id == model.TargetId);

            if (target == null)
            {
                return BadRequest("Target not found");
            }

            var artifact = _deployerContext.Artifacts.FirstOrDefault(x => x.Name == model.Artifact);

            if (artifact == null)
            {
                return BadRequest("Artifact data not found");
            }

            if (artifact.DeployPipe == null || artifact.DeployPipe == "")
            {
                return BadRequest("Specify deploy pipe for this artifact");
            }

            var entryFile = artifact.DeployPipe;

            var artifactPath = PathHelper.GetArtifactPath(model.Artifact, model.Version);

            var fileName = Path.GetFileName(artifactPath);

            var serviceTemplate = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "templateService.service"));
            var serviceText = serviceTemplate.Replace("{{ArtifactName}}", model.Artifact);
            serviceText = serviceText.Replace("{{ExecStart}}", $"/usr/bin/dotnet \"/var/deployerBins/{model.Artifact}/{entryFile}\"");

            var sshClient = target.SshPort.HasValue ?  new SshClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new SshClient(target.HostName, target.SshUser, target.SshPassword);
            var scpClient = target.SshPort.HasValue ?  new ScpClient(target.HostName, target.SshPort.Value, target.SshUser, target.SshPassword) : new ScpClient(target.HostName, target.SshUser, target.SshPassword);

            sshClient.Connect();
            scpClient.Connect();

            sshClient.RunCommand("mkdir /var/deployerBins");
            sshClient.RunCommand($"rm -rf /var/deployerBins/{model.Artifact}");
            sshClient.RunCommand($"mkdir /var/deployerBins/{model.Artifact}");





            using var s = System.IO.File.Open(artifactPath, FileMode.Open);
            scpClient.Upload(s, $"/var/deployerBins/{model.Artifact}/{fileName}");
            s.Close();

            var cmd = sshClient.RunCommand($"cd /var/deployerBins/{model.Artifact} && unzip {fileName}");
            sshClient.RunCommand($"rm -f {fileName}");
            var r = cmd.Result;
            StopService(sshClient, model.Artifact);
            UploadService(scpClient, serviceText, model.Artifact);

            sshClient.RunCommand($"systemctl enable {model.Artifact}");
            sshClient.RunCommand($"systemctl start {model.Artifact}");
            sshClient.RunCommand($"systemctl daemon-reload");

            return Ok();
        }

        private void StopService(SshClient sshClient, string artifact)
        {
            sshClient.RunCommand($"systemctl stop {artifact} >/dev/null 2>&1");
        }

        private void UploadService(ScpClient scpClient, string serviceContent, string artifact)
        {
            scpClient.Upload(GenerateStreamFromString(serviceContent), $"/etc/systemd/system/{artifact}.service");
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
    }



    public class ArtifactDeployDto
    {
        public string Artifact { get; set; }
        public string Version { get; set; }
        public int TargetId { get; set; }
    }
}
