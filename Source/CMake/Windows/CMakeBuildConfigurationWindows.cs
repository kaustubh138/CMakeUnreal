using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal
{
    public record CMakeBuildConfigurationWindows
        : ICMakeBuildConfiguration
    {
        public CMakeBuildConfigurationWindows(string buildDir, string installPath, string thirdPartyPath, string workingDir, string buildType)
        {
            BuildDir = buildDir;
            InstallPath = installPath;
            ThirdPartyPath = thirdPartyPath;
            WorkingDir = workingDir;
            BuildType = buildType;
        }
    }
}
