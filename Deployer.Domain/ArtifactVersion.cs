using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Domain
{
    public class ArtifactVersion : BaseDomainEntity
    {
        public string Version { get; set; }
        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
        public Guid Guid { get; set; }
        public string ChannelId { get; set; }

        public string FileName => System.IO.Path.GetFileName(Path);

        public Artifact Artifact { get; set; }
        public int ArtifactId { get; set; }
    }
}
