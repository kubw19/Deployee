using Deployer.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Jobs.DTOS
{
    public class InputPropertyDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public SpecialTypeDto SpecialType { get; set; }
    }
}
