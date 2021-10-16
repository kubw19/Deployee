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
using Deployer.Logic.Releases.DTOS;
using Deployer.Domain.Release;
using Deployer.Domain.Targets;
using AutoMapper.EquivalencyExpression;

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

            CreateMap<Release, ReleaseReadDto>().AfterMap((src, dst) =>
            {
                var lastDeploy = src.Deploys.OrderByDescending(x => x.LastRunDate).FirstOrDefault();
                dst.LastDeployDate = lastDeploy?.LastRunDate;
                dst.LastDeployId = lastDeploy?.Id;
                dst.LastDeploySuccess = lastDeploy?.IsSuccess;
                dst.LastDeployLog = lastDeploy?.Log;
            });

            CreateMap<TargetRole, TargetRoleDto>();

            CreateMap<InputProperty, InputPropertyDto>().EqualityComparison((a,b)=>a.Name + a.Type == b.Name + b.Type);//.ForMember(x => x.SpecialType, d => d.UseDestinationValue());

            CreateMap<InputPropertyDto, InputProperty>();
        }
    }
}
