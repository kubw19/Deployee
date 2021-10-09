using Deployer.API.Models;
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

    public class PostModel
    {
        public IFormFile File { get; set; }
        public string Version { get; set; }
        public string EntryFile { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly DeployerContext deployerContext;

        public PackagesController(IWebHostEnvironment environment, DeployerContext deployerContext)
        {
            hostingEnvironment = environment;
            this.deployerContext = deployerContext;
        }

        [HttpPost]
        public IActionResult Create(IFormCollection data, IFormFile file)
        {

            PostModel model;
            try
            {
                model = JsonConvert.DeserializeObject<PostModel>(data["data"]);
            }
            catch
            {
                return BadRequest();
            };

            if (model.EntryFile == null || model.EntryFile == "")
            {
                return BadRequest();
            }

            var splitted = file.FileName.Split("__");
            if (splitted.Length < 2)
            {
                return BadRequest();
            }
            var artifactName = splitted[0];
            var version = splitted[1];

            // do other validations on your model as needed
            if (file != null)
            {
                var uniqueFileName = file.FileName;
                var newDir = Path.Combine(PathHelper.PackagesPath, artifactName);

                Directory.CreateDirectory(newDir);

                newDir = Path.Combine(newDir, Path.GetFileNameWithoutExtension(version));

                Directory.CreateDirectory(newDir);

                var filePath = Path.Combine(newDir, uniqueFileName);

                using (var f = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(f);
                }

            }

            var toDelete = deployerContext.Artifacts.Where(x => x.Name == artifactName).SingleOrDefault();
            if (toDelete != null)
            {
                deployerContext.Remove(toDelete);
            }

            var c = new Artifact
            {
                //DeployPipe = model.EntryFile,
                Name = artifactName
            };

            deployerContext.Add(c);
            deployerContext.SaveChanges();

            return Ok();
        }


        [HttpGet("artifacts")]
        public IActionResult GetArtifacts()
        {
            return Ok(
                Directory.GetDirectories(PathHelper.PackagesPath).Select(x => x.Split(Path.DirectorySeparatorChar).Last())
                .Select(x => new { Name = x }).ToList()
                );

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
