using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Jobs.DTOS
{
    public class TargetDto
    {
        public string Name { get; set; }
        public string HostName { get; set; }
        public int? SshPort { get; set; }
        public string SshUser { get; set; }
        public string SshPassword { get; set; }
    }
}
