using Deployer.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Jobs.Steps.Options
{
    public class OptionsBase
    {
        [SpecialField(SpecialFieldEnum.TargetRole)]
        public int TargetRoleId { get; set; }
    }
}
