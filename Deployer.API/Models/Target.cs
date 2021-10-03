using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Models
{
    public class Target
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastVersion { get; set; }
        public string HostName { get; set; }
        public int? SshPort { get; set; }
        public string SshUser { get; set; }
        public string SshPassword { get; set; }
    }
}
