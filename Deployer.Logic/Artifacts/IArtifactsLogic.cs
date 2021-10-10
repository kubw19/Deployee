using Deployer.Foundation;
using Deployer.Logic.Artifacts.DTOS;
using System.Collections.Generic;

namespace Deployer.Logic.Artifacts
{
    public interface IArtifactsLogic
    {
        List<ArtifactReadDto> GetArtifactsForProject(int projectId);
        StatusResponse<string> GetNewFilePath(string fileName, out FileNameMetadataDto filenameMetadata);
        StatusResponse UploadArtifact(string filePath, FileNameMetadataDto filenameMetadata);
    }
}