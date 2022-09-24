using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnrealBuildTool;

public class CMakeUnreal
    : ModuleRules
{
    public CMakeUnreal(ReadOnlyTargetRules Target)
        : base(Target)
    {
        PublicDependencyModuleNames.AddRange(new string[] { "Core", "Engine", "InputCore" });
        PrivateDependencyModuleNames.AddRange(new string[] { "CoreUObject", "Engine" });
    }
}
