using AutoMapper;
using Deployer.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deployer.DatabaseModel;
using Deployer.Logic.Targets.DTOS;
using Deployer.Logic.Targets;

namespace Deployer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TargetsController : ControllerBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly IMapper _mapper;
        private readonly ITargetsLogic _targetsLogic;

        public TargetsController(DeployerContext deployerContext, IMapper mapper, ITargetsLogic targetsLogic)
        {
            _deployerContext = deployerContext;
            _mapper = mapper;
            _targetsLogic = targetsLogic;
        }

        [HttpPost]
        public IActionResult NewTarget(TargetCreateDto model)
        {
            _targetsLogic.AddTarget(model);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _targetsLogic.GetAllTargets();
            return Ok(results);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            _targetsLogic.Remove(id);

            return Ok();
        }

    }
}
