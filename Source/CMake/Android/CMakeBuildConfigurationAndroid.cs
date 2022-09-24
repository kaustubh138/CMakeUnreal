using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal.Android
{
    public record CMakeBuildConfigurationAndroid
        : ICMakeBuildConfiguration
    {
        public CMakeBuildConfigurationAndroid(string buildDir, string installPath, string thirdPartyPath, string workingDir, string buildType, string androidNDKPath, string androidToolchain = "android.toolchain.cmake")
        {
            BuildDir = buildDir;
            InstallPath = installPath;
            ThirdPartyPath = thirdPartyPath;
            WorkingDir = workingDir;
            BuildType = buildType;
            android_ndk_path = androidNDKPath;
            android_toolchain_path = System.IO.Path.Combine(android_ndk_path, androidToolchain);
        }

        public string android_ndk_path;
        public string android_toolchain_path;
    }
}
