using Deployer.Domain;
using Deployer.Domain.Release;
using Deployer.Jobs;
using Deployer.Jobs.Steps;
using Deployer.Repositories.Projects;
using Deployer.Repositories.Releases;
using Deployer.Repositories.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Jobs.Logic
{
    public class DeployLogic : IDeployLogic
    {
        private readonly IJobManager _jobManager;
        private readonly ITargetsRepository _targetsRepository;
        private readonly IReleasesRepository _releasesRepository;
        private readonly IProjectsRepository _projectsRepository;

        public DeployLogic(IJobManager jobManager, ITargetsRepository targetsRepository, IReleasesRepository releasesRepository, IProjectsRepository projectsRepository)
        {
            _jobManager = jobManager;
            _targetsRepository = targetsRepository;
            _releasesRepository = releasesRepository;
            _projectsRepository = projectsRepository;
        }

        public string Deploy(int releaseId)
        {
            var deployPipeContext = new DeployPipeContext(releaseId, _targetsRepository, _releasesRepository);

            var project = _projectsRepository.GetProjectByReleaseId(releaseId);

            var log = "";

            foreach (var step in project.DeploySteps)
            {
                log += _jobManager.DoJob(step, deployPipeContext);
            }

            var hasError = deployPipeContext.IsError;

            var newDeploy = new ReleaseDeploy
            {
                IsSuccess = !hasError,
                LastRunDate = DateTime.UtcNow,
                Log = log,
                ReleaseId = releaseId
            };

            _releasesRepository.AddNewReleaseDeploy(newDeploy);
            _releasesRepository.SaveChanges();

            return log;
        }
    }
}
