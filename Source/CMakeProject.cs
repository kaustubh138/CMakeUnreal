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
        x64
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

        private SystemArch _architecture = SystemArch.x64;
    }
}
