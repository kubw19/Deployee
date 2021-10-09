using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Deployer.API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DeployStep> DeploySteps { get; set; }
    }
}
