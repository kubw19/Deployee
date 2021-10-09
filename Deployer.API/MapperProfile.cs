using AutoMapper;
using Deployer.API.Controllers;
using Deployer.API.Jobs;
using Deployer.API.Jobs.DTOS;
using Deployer.API.Models;
using Deployer.API.Targets.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Target, TargetReadDto>();
            CreateMap<TargetCreateDto, Target>();

            CreateMap<InsertOrUpdateStepDto, DeployStep>();
            CreateMap<StepInfo, StepReadDto>();
            CreateMap<DeployStep, StepReadDto>();
        }
    }
}
