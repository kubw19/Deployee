using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Deployer.Foundation
{
    public static class PathHelper
    {
        public static string HomeDir { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static string DeployerPath => Path.Combine(HomeDir, "Deployer");
        public static string UploadPath => Path.Combine(DeployerPath, "Upload");
        public static string PackagesPath => Path.Combine(DeployerPath, "Packages");

        public static string GetArtifactVersionPath(string artifact, string version)
        {
            var path = Path.Combine(PackagesPath, artifact);
            path = Path.Combine(path, version);
            return Directory.GetFiles(path).First();
        }

        public static string GetArtifactPath(string artifact)
        {
            var path = Path.Combine(PackagesPath, artifact);
            return Directory.GetFiles(path).First();
        }

        public static void EnsureFoldersCreated()
        {
            Directory.CreateDirectory(DeployerPath);
            Directory.CreateDirectory(PackagesPath);
            Directory.CreateDirectory(UploadPath);
        }
    }
}
