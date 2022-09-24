using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal
{
    public record ICMakeBuildConfiguration
    {
        public string BuildDir = "";
        public string InstallPath = "";
        public string ThirdPartyPath = "";
        public string WorkingDir = "";
        public string BuildType = "";
    }
}
