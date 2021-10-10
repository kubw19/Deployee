using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deployer.Domain
{
    public class Artifact : BaseDomainEntity
    {        
        public string Name { get; set; }

        public List<ArtifactVersion> Versions { get; set; } = new List<ArtifactVersion>();

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
