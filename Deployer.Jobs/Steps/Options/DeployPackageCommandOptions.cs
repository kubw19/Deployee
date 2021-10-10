using System.Collections.Generic;

namespace Deployer.Jobs.Steps.Options
{

    public class DeployPackageCommandOptions : IStepOptions
    {
        public string PackageName { get; set; } = "";
        //public string Version { get; set; } = "";

        public IEnumerable<string> OutputVariables { get; } = new List<string>
            {
                "DeployDirectory"
            };
    }

}
