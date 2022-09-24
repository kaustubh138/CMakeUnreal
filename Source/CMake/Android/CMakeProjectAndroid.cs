using CMakeUnreal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal.Android
{
    public record CMakeProjectAndroid
        : ICMakeProject
    {
        public CMakeProjectAndroid(string projectName, string projectSource, string androidNdkPath, string toolchain = "android.toolchain.cmake",string androidAbi = "", string buildType = "\"Release\"", string generator = "MinGW Makefiles", SystemArch arch = SystemArch.Android)
        {
            ProjectName = projectName;
            ProjectSource = projectSource;
            BuildType = buildType;
            Generator = generator;
            android_abi = androidAbi;
        }

        public string android_abi { get; set; }
    }
}
