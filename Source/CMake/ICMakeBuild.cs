using CMakeUnreal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMakeUnreal
{
    public abstract class ICMakeBuild
    {
        protected const string _program = "cmake.exe";
        protected void Build(ICMakeBuildConfiguration conf, CMakeProjectWindows proj)
        {
            conf.BuildDir = string.Format(conf.BuildDir, proj.ProjectName);
            string buildPath = System.IO.Path.Combine(conf.ThirdPartyPath, "Generated", conf.BuildDir);
            string installPath = buildPath;

            int retCode = 0;
            //string buildCommand1 = CreateCMakeBuildCommand(buildPath);
            string buildCommand = CreateCMakeBuildCommand(buildPath, installPath);
            retCode = ExecuteCommandSync(buildCommand, conf.WorkingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot configure CMake project. Exited with code: {retCode}");

            string installCommand = CreateCMakeInstallCommand(conf.ThirdPartyPath, conf.BuildDir, proj.BuildType);
            retCode = ExecuteCommandSync(installCommand, conf.WorkingDir);
            if (retCode != 0)
                Console.WriteLine($@"Cannot build project. Exited with code: {retCode}");

        }

        protected int ExecuteCommandSync(string command, string? workingDir)
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

        public abstract string CreateCMakeBuildCommand(string buildDirectory, string installPath);

        public abstract string CreateCMakeInstallCommand(string thirdPartyPath, string buildDirectory, string buildType);
    }
}
