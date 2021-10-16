using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deployer.Domain
{
    public class InputProperty : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public DeployStep DeployStep { get; set; }
        public int DeployStepId { get; set; }
    }
}
