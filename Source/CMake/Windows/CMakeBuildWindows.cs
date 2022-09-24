using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CMakeUnreal
{
    public class CMakeBuildWindows
        : ICMakeBuild
    {
        private CMakeBuildConfigurationWindows _config;
        private CMakeProjectWindows _project;

        public CMakeBuildWindows(CMakeBuildConfigurationWindows config, CMakeProjectWindows proj)
        {
            _config = config;
            _project = proj;
            string command = CreateCMakeBuildCommand(config.BuildDir, config.InstallPath);
            ExecuteCommandSync(command, proj.ProjectSource);
        }

        public override string CreateCMakeBuildCommand(string buildDirectory, string installPath)
        {
            string arguments = $@"-G {_project.Generator} " +
                                $@"-S {"\""}{_project.ProjectSource}{"\""} " +
                                $@"-B {"\""}{_config.BuildDir}{"\""} " +
                                $@"-A {_project.Arch.ToString()} " +
                                $@"-T host={_project.Arch.ToString()} " +
                                $@"-DCMAKE_BUILD_TYPE={_project.BuildType} " +
                                $@"-DCMAKE_INSTALL_PREFIX={_config.InstallPath}" +
                                "-MD";

            return ICMakeBuild._program + " " + arguments;
        }

        public override string CreateCMakeInstallCommand(string thirdPartyPath, string buildDirectory, string buildType)
        {
            return $@"{_program} --build {"\""}{Path.Combine(thirdPartyPath, "Generated", buildDirectory)}{"\""} " +
                   $@"--target install " +
                   $@"--config {buildType} ";
        }
    }
}