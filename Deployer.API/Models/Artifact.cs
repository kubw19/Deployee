using System.ComponentModel.DataAnnotations;

namespace Deployer.API.Models
{
    public class Artifact
    {
        [Key]
        public string Name { get; set; }
        public string DeployPipe { get; set; }
    }
}
