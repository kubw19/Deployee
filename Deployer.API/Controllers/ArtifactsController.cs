using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Logic.Artifacts;
using Deployer.Logic.Artifacts.DTOS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ArtifactsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DeployerContext _deployerContext;
        private readonly IArtifactsLogic _artifactsLogic;

        public ArtifactsController(IWebHostEnvironment environment, DeployerContext deployerContext, IArtifactsLogic artifactsLogic)
        {
            _hostingEnvironment = environment;
            this._deployerContext = deployerContext;
            _artifactsLogic = artifactsLogic;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult Upload(IFormCollection data, IFormFile file)
        {
            FileNameMetadataDto metadata;
            var path = _artifactsLogic.GetNewFilePath(file.FileName, out metadata).Value;

            using (var f = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(f);
            }

            var result = _artifactsLogic.UploadArtifact(path, metadata);

            return HttpHelper.ReturnByLogicResponse(result);

        }

        [HttpGet("projects/{projectId}")]
        public IActionResult GetArtifacts(int projectId)
        {
            var artifacts = _artifactsLogic.GetArtifactsForProject(projectId);

            return Ok(artifacts);

        }
        [HttpGet("artifacts/{name}/versions")]
        public IActionResult GetVersions(string name)
        {
            return Ok(
                Directory.GetDirectories(Path.Combine(PathHelper.PackagesPath, name)).Select(x => x.Split(Path.DirectorySeparatorChar).Last())
                .Select(x => new { Name = x }).ToList()
                );

        }
    }
}
