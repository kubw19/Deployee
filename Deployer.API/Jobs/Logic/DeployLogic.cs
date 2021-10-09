using Deployer.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.API.Jobs.Logic
{
    public class DeployLogic
    {
        private readonly JobManager _jobManager;

        public DeployLogic(JobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public void Deploy(Project project)
        {
            foreach(var step in project.DeploySteps)
            {
                _jobManager.DoJob(step, new DeployPipeContext());
            }
        }
    }
}
