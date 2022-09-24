using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace CMakeUnreal
{
    public class CMakeBuild
    {
        public CMakeBuild(CMakeProject proj, string thirdPartyPath, string workingDir)
        {
            _project = proj;
            _buildDir = $@"build-{_project.ProjectName}";
            _thirdPartyPath = thirdPartyPath;
            Build(proj, thirdPartyPath, workingDir);
        }

        private string _thirdPartyPath;
        private const string _program = "cmake.exe";
        private CMakeProject _project;
        private string _buildDir;

        private void Build(CMakeProject proj, string thirdPartyPath, string workingDir)
        {
            _buildDir = string.Format(_buildDir, proj.ProjectName);
            string buildPath = System.IO.Path.Combine(thirdPartyPath, "Generated", _buildDir);
            string installPath = buildPath;

            int retCode = 0;
            //string buildCommand1 = CreateCMakeBuildCommand(buildPath);
            string buildCommand = CreateCMakeBuildCommand(buildPath, installPath);
            retCode = ExecuteCommandSync(buildCommand, workingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot configure CMake project. Exited with code: {retCode}");

            string installCommand = CreateCMakeInstallCommand(_buildDir, proj.BuildType);
            retCode = ExecuteCommandSync(installCommand, workingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot build project. Exited with code: {retCode}");

        }

        public int ExecuteCommandSync(string command, string? workingDir)
        {
            Console.WriteLine("Running: " + command);
            System.Diagnostics.ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = workingDir
            };

            StringBuilder sb = new StringBuilder();
            Process? p = Process.Start(processInfo);
            if (p == null)
            {
                Console.WriteLine($"Failed to {0} create process!", "cmd");
                return -1;
            }


            p.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            p.BeginOutputReadLine();
            p.WaitForExit();

            return p.ExitCode;
        }

        private string CreateCMakeBuildCommand(string buildDirectory, string installPath)
        {
            string arguments = $@"-G {_project.Generator} " +
                                $@"-S {"\""}{_project.ProjectSource}{"\""} " +
                                $@"-B {"\""}{buildDirectory}{"\""} " +
                                $@"-A {_project.Arch.ToString()} " +
                                $@"-T host={_project.Arch.ToString()} " +
                                $@"-DCMAKE_BUILD_TYPE={_project.BuildType} " +
                                $@"-DCMAKE_INSTALL_PREFIX={installPath}" +
                                "-MD";

            return _program + " " + arguments;
        }

        private string CreateCMakeInstallCommand(string buildDirectory,
                                                        string buildType)
        {
            return $@"{_program} --build {"\""}{System.IO.Path.Combine(_thirdPartyPath, "Generated", buildDirectory)}{"\""} " +
                    $@"--target install " +
                    $@"--config {buildType} ";
        }

    }

}