using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CMakeUnreal.Android
{
    public class CMakeBuildAndroid
        : ICMakeBuild
    {
        private CMakeBuildConfigurationAndroid _config;
        private CMakeProjectAndroid _project;

        public CMakeBuildAndroid(CMakeBuildConfigurationAndroid config, CMakeProjectAndroid proj)
        {
            _config = config;
            _project = proj;
            string command = CreateCMakeBuildCommand(config.BuildDir, config.InstallPath);
            ExecuteCommandSync(command, proj.ProjectSource);

            string install = CreateCMakeInstallCommand(_config.ThirdPartyPath, _config.BuildDir, _project.BuildType);
            ExecuteCommandSync(install, _config.ThirdPartyPath);
        }

        public override string CreateCMakeBuildCommand(string buildDirectory, string installPath)
        {
            string arguments = $@"-G {"\""}{_project.Generator}{"\""} " +
                                $@"-S {"\""}{_project.ProjectSource}{"\""} " +
                                $@"-B {"\""}{_config.BuildDir}{"\""} " +
                                $@"-DCMAKE_TOOLCHAIN_FILE={"\""}{_config.android_toolchain_path}{"\""} " +
                                $@"-DCMAKE_BUILD_TYPE={_project.BuildType} " +
                                $@"-DCMAKE_INSTALL_PREFIX={"\""}{_config.InstallPath}{"\""}";

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