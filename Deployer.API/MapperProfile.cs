using AutoMapper;
using Deployer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deployer.Jobs.DTOS;
using Deployer.Logic.Steps.DTOS;
using Deployer.Logic.Targets.DTOS;
using Deployer.Logic.Artifacts.DTOS;

namespace Deployer.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Target, TargetReadDto>();
            CreateMap<TargetCreateDto, Target>();

            CreateMap<InsertOrUpdateStepDto, DeployStep>();
            CreateMap<StepInfoDto, StepReadDto>();
            CreateMap<DeployStep, StepReadDto>();

            CreateMap<ArtifactVersion, ArtifactVersionReadDto>();
            CreateMap<Artifact, ArtifactReadDto>();
        }
    }
}
