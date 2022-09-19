using System.Diagnostics;
using System.Text;

namespace CMakeUnreal
{
    public class CMakeBuild
    {
        public CMakeBuild(CMakeProject proj, string thirdPartyPath, string workingDir)
        {
            _project = proj;
            _buildDir = $@"build-{_project.ProjectName}";
            Build(proj, thirdPartyPath, workingDir);
        }

        private void Build(CMakeProject proj, string thirdPartyPath, string workingDir)
        {
            _buildDir = string.Format(_buildDir, proj.ProjectName);
            string buildPath = System.IO.Path.Combine(thirdPartyPath, "Generated", _buildDir);

            int retCode = 0;
            string buildCommand = CreateCMakeBuildCommand(proj.ProjectSource,
                                                            buildPath,
                                                            proj.Generator,
                                                            proj.Arch,
                                                            proj.Arch.ToString(),
                                                            proj.BuildType);
            retCode = ExecuteCommandSync(buildCommand, workingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot configure CMake project. Exited with code: {retCode}");

            string installCommand = CreateCMakeInstallCommand(_buildDir, proj.BuildType);
            retCode = ExecuteCommandSync(installCommand, workingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot build project. Exited with code: {retCode}");

        }

        private const string _program = "cmake.exe";
        private CMakeProject _project;
        private string _buildDir;

        private int ExecuteCommandSync(string command, string? workingDir)
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

        private string CreateCMakeBuildCommand(string sourceDir,
                                                string buildDirectory,
                                                string generator,
                                                string arch,
                                                string installPath,
                                                string buildType)
        {
            string arguments = $@"-G {generator} " +
                               $@"-S {"\""}{sourceDir}{"\""} " +
                               $@"-B {"\""}{buildDirectory}{"\""} " +
                               $@"-A {arch} " +
                               $@"-T host={arch} " +
                               $@"-DCMAKE_BUILD_TYPE={buildType} " +
                               $@"-DCMAKE_INSTALL_PREFIX={installPath}";

            return _program + " " + arguments;
        }

        private string CreateCMakeInstallCommand(string buildDirectory,
                                                        string buildType)
        {
            return $@"{_program} --build {"\""}{buildDirectory}{"\""} " +
                   $@"--target install " +
                   $@"--config {buildType} ";
        }

    }
}