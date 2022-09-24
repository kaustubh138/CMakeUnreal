using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal
{
    public enum SystemArch
    {
        x86,
        x64,
        Android
    }

    public class CMakeProject
    {
        public CMakeProject(string projectName, string projectSource, string buildType = "\"Release\"", string generator = "\"Visual Studio 17 2022\"", SystemArch arch = SystemArch.x64)
        {
            ProjectName = projectName;
            ProjectSource = projectSource;
            BuildType = buildType;
            Generator = generator;
            _architecture = arch;
        }

        public string ProjectName { get; set; }
        public string ProjectSource { get; set; }
        public string BuildType { get; set; } = "\"Release\"";
        public string Generator { get; set; } = "\"Visual Studio 17 2022\"";
        public string Arch
        {
            get { return $@"{_architecture.ToString()}"; }
        }

        protected SystemArch _architecture = SystemArch.x64;
    }

    public class CMakeAndroidProject
        : CMakeProject
    {
        CMakeAndroidProject(string projectName, string projectSource, string androidNdkPath, string androidAbi, string buildType = "\"Release\"", string generator = "Ninja", SystemArch arch = SystemArch.Android)
            : base(projectName, projectSource, buildType, generator, arch)
        {
            android_ndk_path = androidNdkPath;
            android_abi = androidAbi;
        }

        public string android_abi { get; set; }
        public string android_ndk_path { get; set; }
        public string toolchain { get; set; } = "android.toolchain.cmake";
    }

}
