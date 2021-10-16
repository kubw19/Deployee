using Deployer.DatabaseModel;
using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Jobs.Logic;
using Deployer.Logic.Artifacts;
using Deployer.Logic.Artifacts.DTOS;
using Deployer.Logic.Releases;
using Deployer.Logic.Releases.DTOS;
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
    public class ReleasesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IReleasesLogic _releasesLogic;
        private readonly IDeployLogic _deployLogic;

        public ReleasesController(IWebHostEnvironment environment, IReleasesLogic releasesLogic, IDeployLogic deployLogic)
        {
            _hostingEnvironment = environment;
            _releasesLogic = releasesLogic;
            _deployLogic = deployLogic;
        }

        [HttpPost]
        public IActionResult NewRelease(CreateReleaseDto dto)
        {
            var results = _releasesLogic.CreateNewRelease(dto);

            return HttpHelper.ReturnByLogicResponse(results);
        }

        [HttpGet("projects/{projectId}")]
        public IActionResult GetAllForProject(int projectId)
        {
            var results = _releasesLogic.GetAllReleasesForProject(projectId);

            return Ok(results);
        }

        [HttpPost("deploys/{releaseId}")]
        public IActionResult Deploy(int releaseId)
        {
            var result = _deployLogic.Deploy(releaseId);
            return Ok(result);
        }
    }
}
