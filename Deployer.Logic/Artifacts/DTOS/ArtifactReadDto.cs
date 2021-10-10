using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Artifacts.DTOS
{
    public class ArtifactReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ArtifactVersionReadDto> Versions { get; set; }

    }
}
