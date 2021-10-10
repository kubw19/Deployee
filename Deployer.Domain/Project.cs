using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deployer.Domain
{
    public class Project : BaseDomainEntity
    {
        public string Name { get; set; }
        public List<DeployStep> DeploySteps { get; set; } = new List<DeployStep>();
        public List<Artifact> Artifacts { get; set; } = new List<Artifact>();
    }
}
