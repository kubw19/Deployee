using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Models
{
    public class DeployStep
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DeployStepType Type { get; set; }
        public List<InputProperty> InputProperties { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }

    }

    public enum DeployStepType
    {
        DeployPackageCommand,
        RunCommand,
        RunService
    }
}
