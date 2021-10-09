using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deployer.API.Models
{
    public class InputProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public DeployStep DeployStep { get; set; }
        public int DeployStepId { get; set; }
    }
}
