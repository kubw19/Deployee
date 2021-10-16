using Deployer.Foundation;
using System.Collections.Generic;

namespace Deployer.Jobs.Steps.Options
{

    public class DeployPackageCommandOptions : OptionsBase, IStepOptions
    {
        [SpecialField(SpecialFieldEnum.Artifact, "")]
        public int Artifact { get; set; }

        public IEnumerable<string> OutputVariables { get; } = new List<string>
            {
                "DeployDirectory"
            };
    }

}
