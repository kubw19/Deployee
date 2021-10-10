using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Artifacts.DTOS
{
    public class FileNameMetadataDto
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ChannelId { get; set; }
        public Guid Guid { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
