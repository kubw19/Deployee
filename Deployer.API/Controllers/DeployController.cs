using Deployer.DatabaseModel;
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

            //if (artifact.DeployPipe == null || artifact.DeployPipe == "")
            //{
            //    return BadRequest("Specify deploy pipe for this artifact");
            //}

            return Ok();
        }


    }



    public class ArtifactDeployDto
    {
        public string Artifact { get; set; }
        public string Version { get; set; }
        public int TargetId { get; set; }
    }
}
