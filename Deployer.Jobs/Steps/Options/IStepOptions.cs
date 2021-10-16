using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps.Options
{
    public interface IStepOptions
    {
        IEnumerable<string> OutputVariables { get; }
        int TargetRoleId { get; set; }
    }
}
