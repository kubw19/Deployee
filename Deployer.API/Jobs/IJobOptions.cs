using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Jobs
{
    public interface IJobOptions
    {
        IEnumerable<string> OutputVariables { get; }
    }
}
