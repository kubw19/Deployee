using AutoMapper;
using Deployer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deployer.Jobs.DTOS;
using Deployer.DatabaseModel;
using Deployer.Jobs;
using Deployer.Logic.Steps;
using Deployer.Foundation;
using Deployer.Logic.Steps.DTOS;

namespace Deployer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly IMapper _mapper;
        private readonly IJobManager _jobManager;
        private readonly IStepsLogic _stepsLogic;

        public ProjectsController(DeployerContext deployerContext, IMapper mapper, IJobManager jobManager, IStepsLogic stepsLogic)
        {
            _deployerContext = deployerContext;
            _mapper = mapper;
            _jobManager = jobManager;
            _stepsLogic = stepsLogic;
        }

        [HttpGet("StepTemplates/{type}")]
        public IActionResult GetStepTemplate(DeployStepType type)
        {
            var model = _stepsLogic.GetStepTemplate(type);
            return Ok(model);
        }

        [HttpGet("StepTemplates")]
        public IActionResult GetAllAvailableStepTemplates()
        {
            var model = _stepsLogic.GetAllAvailableStepTemplates();
            return Ok(model);
        }

        [HttpPost("Steps")]
        public IActionResult AddNewStep(InsertOrUpdateStepDto dto)
        {
            var response = _stepsLogic.AddNewOrUpdateStep(dto);
            return Ok(response);
        }

        [HttpDelete("Step/{stepId}")]
        public IActionResult DeleteStep(int stepId)
        {
            _stepsLogic.RemoveStep(stepId);
            return Ok();
        }

        [HttpGet("{projectId}/steps")]
        public IActionResult Steps(int projectId)
        {
            var response = _stepsLogic.GetStepsForProject(projectId);
            return Ok(response);
        }

        [HttpGet("steps/{id}")]
        public IActionResult GetOneStep(int id)
        {
            var step = _stepsLogic.GetStep(id);
            return HttpHelper.ReturnByLogicResponse(step);

        }

    }
}
