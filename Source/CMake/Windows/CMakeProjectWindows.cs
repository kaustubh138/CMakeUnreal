using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal
{

    public record CMakeProjectWindows
        : ICMakeProject
    {
        public CMakeProjectWindows(string projectName, string projectSource, string buildType = "\"Release\"", string generator = "\"Visual Studio 17 2022\"", SystemArch arch = SystemArch.x64)
        {
            ProjectName = projectName;
            ProjectSource = projectSource;
            BuildType = buildType;
            Generator = generator;
            _architecture = arch;
        }

        public string Arch
        {
            get { return $@"{_architecture.ToString()}"; }
        }

        protected SystemArch _architecture = SystemArch.x64;
    }
}
