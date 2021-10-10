using AutoMapper;
using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Jobs;
using Deployer.Jobs.DTOS;
using Deployer.Logic.Steps.DTOS;
using Deployer.Repositories.DeploySteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Steps
{
    public class StepsLogic : IStepsLogic
    {
        private readonly IJobManager _jobManager;
        private readonly IMapper _mapper;
        private readonly IDeployStepsRepository _deployStepsRepository;

        public StepsLogic(IJobManager jobManager, IMapper mapper, IDeployStepsRepository deployStepsRepository)
        {
            _jobManager = jobManager;
            _mapper = mapper;
            _deployStepsRepository = deployStepsRepository;
        }
        public StepReadDto GetStepTemplate(DeployStepType type)
        {
            return _mapper.Map<StepReadDto>(_jobManager.GetStepOptions(type));
        }

        public List<StepSimpleDto> GetAllAvailableStepTemplates()
        {
            return _jobManager.GetAvailableSteps();
        }

        public List<StepReadDto> GetStepsForProject(int projectId)
        {
            var steps = _deployStepsRepository.GetStepsByProjectId(projectId);
            List<StepReadDto> dtos = new List<StepReadDto>();
            foreach (var step in steps)
            {
                var data = _jobManager.GetStepOptions(step.Type);
                var dto = _mapper.Map<StepReadDto>(data);
                _mapper.Map(step, dto);
                dtos.Add(dto);
            }

            return dtos;
        }

        public StatusResponse<StepReadDto> GetStep(int stepId)
        {
            var step = _deployStepsRepository.Get(stepId);
            if (step == null)
            {
                return new StatusResponse<StepReadDto>(StatusResponseType.NotFound);
            }

            var data = _jobManager.GetStepOptions(step.Type);
            var dto = _mapper.Map<StepReadDto>(data);
            _mapper.Map(step, dto);

            return new StatusResponse<StepReadDto>(dto);
        }

        public StatusResponse<int> AddNewOrUpdateStep(InsertOrUpdateStepDto dto)
        {
            if (dto.Id.HasValue && dto.Id.Value != 0)
            {
                _deployStepsRepository.RemoveInputPropertiesForDeployStep(dto.Id.Value);

                var step = _deployStepsRepository.Get(dto.Id.Value);
                _mapper.Map(dto, step);
                _deployStepsRepository.SaveChanges();
                return new StatusResponse<int>(dto.Id.Value);
            }

            var model = _mapper.Map<DeployStep>(dto);

            _deployStepsRepository.Add(model);
            _deployStepsRepository.SaveChanges();

            return new StatusResponse<int>(model.Id);
        }

        public StatusResponse RemoveStep(int stepId)
        {
            _deployStepsRepository.Remove(stepId);
            _deployStepsRepository.SaveChanges();
            return new StatusResponse();
        }
    }
}
