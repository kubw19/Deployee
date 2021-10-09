using AutoMapper;
using Deployer.API.Jobs;
using Deployer.API.Jobs.DTOS;
using Deployer.API.Models;
using Deployer.API.Targets.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DeployerContext _deployerContext;
        private readonly IMapper _mapper;
        private readonly IJobManager jobManager;

        public ProjectsController(DeployerContext deployerContext, IMapper mapper, IJobManager jobManager)
        {
            _deployerContext = deployerContext;
            _mapper = mapper;
            this.jobManager = jobManager;
        }

        [HttpGet("StepOptions")]
        public IActionResult GetStepOptions(DeployStepType type)
        {
            var model = _mapper.Map<StepReadDto>(jobManager.GetJobOptions(type));
            return Ok(model);
        }

        [HttpGet("AvailableSteps")]
        public IActionResult AvailableSteps()
        {
            return Ok(jobManager.GetAvailableSteps());
        }

        [HttpPost("NewStep")]
        public IActionResult AddNewStep(InsertOrUpdateStepDto dto)
        {

            if (dto.Id.HasValue && dto.Id.Value != 0)
            {
                _deployerContext.RemoveRange(_deployerContext.InputProperties.Where(x => x.DeployStepId == dto.Id.Value));

                var step = _deployerContext.DeploySteps.FirstOrDefault(x => x.Id == dto.Id.Value);
                _mapper.Map(dto, step);
                _deployerContext.SaveChanges();
                return Ok();
            }

            var model = _mapper.Map<DeployStep>(dto);

            _deployerContext.DeploySteps.Add(model);
            _deployerContext.SaveChanges();

            return Ok(new { Id = model.Id});
        }

        [HttpDelete("DeleteStep")]
        public IActionResult DeleteStep(int stepId)
        {

            var toRemove = _deployerContext.DeploySteps.SingleOrDefault(x => x.Id == stepId);

            _deployerContext.Remove(toRemove);
            _deployerContext.SaveChanges();

            return Ok();
        }

        [HttpGet("currentSteps")]
        public IActionResult CurrentSteps(int projectId)
        {
            var steps = _deployerContext.DeploySteps.Include(x => x.InputProperties).Where(x => x.ProjectId == projectId).ToList();

            List<StepReadDto> dtos = new List<StepReadDto>();
            foreach (var step in steps)
            {
                var data = jobManager.GetJobOptions(step.Type);
                var dto = _mapper.Map<StepReadDto>(data);
                _mapper.Map(step, dto);
                dtos.Add(dto);
            }

            return Ok(dtos.ToList());
        }

        [HttpGet("steps/{id}")]
        public IActionResult GetOneStep(int id)
        {
            var step = _deployerContext.DeploySteps.Include(x => x.InputProperties).SingleOrDefault(x => x.Id == id);
            var data = jobManager.GetJobOptions(step.Type);
            var dto = _mapper.Map<StepReadDto>(data);
            _mapper.Map(step, dto);

            return Ok(dto);
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var models = _deployerContext.Targets;
            var mapped = _mapper.Map<IEnumerable<TargetReadDto>>(models);
            return Ok(mapped);
        }
    }

    public class InsertOrUpdateStepDto
    {
        public int? Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DeployStepType Type { get; set; }
        public List<InputProperty> InputProperties { get; set; }
    }
}
