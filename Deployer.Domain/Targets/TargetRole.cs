using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Domain.Targets
{
    public class TargetRole : BaseDomainEntity
    {
        public string Name { get; set; }
        public List<Target> Targets { get; set; }
    }
}
