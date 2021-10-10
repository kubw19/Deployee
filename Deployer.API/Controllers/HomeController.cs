using Deployer.Domain;
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
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        public HomeController(IWebHostEnvironment environment)
        {
            hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Sample()
        //{
        //    var cos = new DeployPipe()
        //    {
        //        Name = "Pierwszy",
        //        ProjectId = 1,
        //        Steps = new List<DeployStep>
        //        {
        //            new DeployStep
        //            {
        //                Name = "Upload paczki",
        //                Type = DeployStepType.DeployPackageCommand,
        //                Options = JsonConvert.SerializeObject(new DeployPackageCommandOptions
        //                {
        //                    Name = "worktimemanager"
        //                })
        //            },
        //            new DeployStep
        //            {
        //                Name = "Upload paczki",
        //                Type = DeployStepType.RunCommand,
        //                Options = JsonConvert.SerializeObject(new RunServiceOptions
        //                {
        //                    Name = "worktimemanager",
        //                    Description = "Opis"                            
        //                })
        //            }
        //        }
        //    };

        //    return Json(cos);
        //}

    }
}
