using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Artifacts.DTOS
{
    public class ArtifactVersionReadDto
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string ChannelId { get; set; }
    }
}
