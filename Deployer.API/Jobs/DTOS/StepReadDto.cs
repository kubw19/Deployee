using Deployer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Jobs.DTOS
{
    public class StepReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public DeployStepType Type { get; set; }
        public string TypeName { get; set; }
        public List<InputProperty> InputProperties { get; set; }
        public List<string> OutputVariables { get; set; }
    }
}
