using System;

namespace Deployer.Jobs.DTOS
{
    public class CommandOptionsPairDto
    {
        public Type Command { get; set; }
        public Type Options { get; set; }
    }
}
