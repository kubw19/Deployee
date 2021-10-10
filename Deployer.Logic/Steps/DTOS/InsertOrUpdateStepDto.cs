using Deployer.Domain;
using System.Collections.Generic;

namespace Deployer.Logic.Steps.DTOS
{
    public class InsertOrUpdateStepDto
    {
        public int? Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DeployStepType Type { get; set; }
        public List<InputProperty> InputProperties { get; set; }
    }
}
