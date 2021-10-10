using Deployer.Domain;
using System.Collections.Generic;

namespace Deployer.Jobs.DTOS
{
    public class StepInfoDto
    {
        public DeployStepType Type { get; set; }
        public string TypeName { get; set; }
        public List<InputProperty> InputProperties { get; set; }
        public List<string> OutputVariables { get; set; }
    }
}

