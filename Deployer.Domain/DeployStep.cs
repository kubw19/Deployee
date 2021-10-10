using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Domain
{
    public class DeployStep : BaseDomainEntity
    {
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
