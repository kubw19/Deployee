using AutoMapper;
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
        }
    }
}
