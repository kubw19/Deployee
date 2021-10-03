using AutoMapper;
using Deployer.API.Models;
using Deployer.API.Targets.DTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TargetsController : ControllerBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly IMapper _mapper;

        public TargetsController(DeployerContext deployerContext, IMapper mapper)
        {
            _deployerContext = deployerContext;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult NewTarget(TargetCreateDto model)
        {
            var dbModel = _mapper.Map<Target>(model);
            _deployerContext.Add(dbModel);
            _deployerContext.SaveChanges();
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var models = _deployerContext.Targets;
            var mapped = _mapper.Map<IEnumerable<TargetReadDto>>(models);
            return Ok(mapped);
        }
    }
}
