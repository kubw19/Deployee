using System.Collections.Generic;

namespace Deployer.Jobs.Steps.Options
{
    public class RunServiceOptions : OptionsBase, IStepOptions
    {
        public string ExecServiceCommand { get; set; } = "/usr/bin/dotnet \"{{DeployDirectory}}/file.dll\"";
        public string WorkDir { get; set; } = "{{DeployDirectory}}";
        public string Name { get; set; } = "";
        public string User { get; set; } = "root";
        public string Description { get; set; }

        public IEnumerable<string> OutputVariables { get; } = new List<string>
        {

        };
    }
}
