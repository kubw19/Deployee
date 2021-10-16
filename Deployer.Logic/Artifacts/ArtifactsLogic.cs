using AutoMapper;
using Deployer.Domain;
using Deployer.Foundation;
using Deployer.Logic.Artifacts.DTOS;
using Deployer.Repositories.Artifacts;
using Deployer.Repositories.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Logic.Artifacts
{
    public class ArtifactsLogic : IArtifactsLogic
    {
        private readonly IArtifactsRepository _artifactsRepository;
        private readonly IProjectsRepository _projectsRepository;
        private readonly IMapper _mapper;

        public ArtifactsLogic(IArtifactsRepository artifactsRepository, IProjectsRepository projectsRepository, IMapper mapper)
        {
            _artifactsRepository = artifactsRepository;
            _projectsRepository = projectsRepository;
            _mapper = mapper;
        }

        public StatusResponse<string> GetNewFilePath(string fileName, out FileNameMetadataDto filenameMetadata)
        {
            filenameMetadata = GetMetadataFromFilename(fileName);

            if (filenameMetadata == null)
            {
                return new StatusResponse<string>(StatusResponseType.BadRequest);
            }

            var uniqueFileName = $"{Guid.NewGuid()}__{fileName}";
            var newDir = Path.Combine(PathHelper.PackagesPath, filenameMetadata.Name);

            Directory.CreateDirectory(newDir);

            newDir = Path.Combine(newDir, Path.GetFileNameWithoutExtension(filenameMetadata.Version));

            Directory.CreateDirectory(newDir);

            var filePath = Path.Combine(newDir, uniqueFileName);

            return new StatusResponse<string>(filePath);
        }

        private FileNameMetadataDto GetMetadataFromFilename(string fileName)
        {
            var splitted = Path.GetFileNameWithoutExtension(fileName).Split("__");
            if (splitted.Length < 2 || splitted.Length > 3)
            {
                return null;
            }

            return new FileNameMetadataDto
            {
                Name = splitted[0],
                Version = splitted[1],
                ChannelId = splitted.Length == 3 ? splitted[2] : "default"
            };
        }

        public StatusResponse UploadArtifact(string filePath, FileNameMetadataDto metadata)
        {
            var filename = Path.GetFileName(filePath);

            var guid = Guid.Parse(filename.Split("__")[0]);

            if (metadata == null)
            {
                return new StatusResponse(StatusResponseType.BadRequest);
            }

            var existingArtifact = _artifactsRepository.GetByArtifactName(metadata.Name);
            if (existingArtifact == null)
            {
                existingArtifact = new Artifact
                {
                    Name = metadata.Name
                };
                _artifactsRepository.Add(existingArtifact);

                ///temp
                var project = _projectsRepository.Get(1);
                project.Artifacts.Add(existingArtifact);
                //endtemp

            }

            var existingVersion = _artifactsRepository.GetArtifactVersionByName(metadata.Version);

            if (existingVersion == null)
            {
                var newVersion = new ArtifactVersion
                {
                    ChannelId = metadata.ChannelId,
                    Path = filePath,
                    Version = metadata.Version,
                    UploadTime = DateTime.UtcNow,
                    Guid = guid
                };

                existingArtifact.Versions.Add(newVersion);
            }
            else
            {
                existingVersion.ChannelId = metadata.ChannelId;
                existingVersion.Path = filePath;
                existingVersion.Version = metadata.Version;
                existingVersion.UploadTime = DateTime.UtcNow;
                existingVersion.Guid = guid;                  
            }

            _artifactsRepository.SaveChanges();

            return new StatusResponse(StatusResponseType.Ok);

        }
    
    
        public List<ArtifactReadDto> GetArtifactsForProject(int projectId)
        {
            var artifacts = _artifactsRepository.GetArtifactsForProject(projectId);
            var mapped = _mapper.Map<List<ArtifactReadDto>>(artifacts);

            return mapped;
        }
    }
}
