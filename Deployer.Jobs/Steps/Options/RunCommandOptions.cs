using System.Collections.Generic;

namespace Deployer.Jobs.Steps.Options
{
    public class RunCommandOptions : OptionsBase, IStepOptions
    {
        public string Command { get; set; } = "echo hej";

        public IEnumerable<string> OutputVariables => new List<string>();
    }
}
