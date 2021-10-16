using AutoMapper;
using Deployer.Domain.Targets;
using Deployer.Foundation;
using Deployer.Logic.Targets.DTOS;
using Deployer.Repositories.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Targets
{
    public class TargetsLogic : ITargetsLogic
    {
        private readonly IMapper _mapper;
        private readonly ITargetsRepository _targetsRepository;

        public TargetsLogic(IMapper mapper, ITargetsRepository targetsRepository)
        {
            _mapper = mapper;
            _targetsRepository = targetsRepository;
        }
        public void AddTarget(TargetCreateDto target)
        {
            var dbModel = _mapper.Map<Target>(target);

            var role = _targetsRepository.GetTargetRoleById(1);
            dbModel.TargetRoles = new List<TargetRole>();
            dbModel.TargetRoles.Add(role);


            _targetsRepository.Add(dbModel);
            _targetsRepository.SaveChanges();
        }

        public List<TargetReadDto> GetAllTargets()
        {
            var models = _targetsRepository.GetAll();
            return _mapper.Map<List<TargetReadDto>>(models);
        }

        public StatusResponse Remove(int id)
        {
            _targetsRepository.Remove(id);
            _targetsRepository.SaveChanges();

            return new StatusResponse(StatusResponseType.Ok);
        }

        public List<TargetReadDto> GetTargetsForRole(int targetRoleId)
        {
            var models = _targetsRepository.GetTargetsForRoleId(targetRoleId);
            return _mapper.Map<List<TargetReadDto>>(models);
        }

        public List<TargetRoleDto> GetTargetRoles()
        {
            var models = _targetsRepository.GetAllTargetRoles();
            return _mapper.Map<List<TargetRoleDto>>(models);
        }
    }
}
