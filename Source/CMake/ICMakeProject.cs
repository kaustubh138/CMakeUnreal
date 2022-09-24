using System;
using System.Collections.Generic;
using System.Linq;
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

    public record ICMakeProject
    {
        public string ProjectName   = "";
        public string ProjectSource = "";
        public string BuildType     = "";
        public string Generator     = "";
    }
}
