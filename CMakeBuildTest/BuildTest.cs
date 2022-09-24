namespace CMakeBuildTest;
using CMakeUnreal;
using CMakeUnreal.Android;
using System.IO;

[TestClass]
public class BuildTest
{
    [TestMethod]
    public void WindowsBuildTest()
    {
        // Get path of test project
        var Path = System.IO.Path.Combine(
            VisualStudioProvider.TryGetSolutionDirectoryInfo().Parent.FullName, "CMakeUnreal", "DummyCMakeProject");
        string ThirdPartyPath = Path;
        CMakeProjectWindows testProj = new CMakeProjectWindows("Dummy", Path);
        string buildDir = System.IO.Path.Combine(Path, "Generated", $@"build-{testProj.ProjectName}");
        string installPath = buildDir;
        CMakeBuildConfigurationWindows testConf = new CMakeBuildConfigurationWindows(buildDir, installPath, ThirdPartyPath, Path, testProj.BuildType);
        new CMakeBuildWindows(testConf, testProj);

        var folder = new System.IO.DirectoryInfo(System.IO.Path.Combine(Path, System.IO.Path.Combine("Generated", $@"build-{testProj.ProjectName}")));
        if (folder.Exists)
            Assert.AreNotEqual(folder.GetFileSystemInfos().Length, 0);
        else
            Assert.AreEqual(true, false);
    }
    
    [TestMethod]
    public void AndroidBuildTest()
    {
        // Get path of test project
        var Path = System.IO.Path.Combine(
            VisualStudioProvider.TryGetSolutionDirectoryInfo().Parent.FullName, "CMakeUnreal", "DummyCMakeProject");
        
        string ThirdPartyPath = Path;
        string ndkPath = System.IO.Path.Combine("C:", "Users", "Kaustubh", "AppData", "Local", "Android", "Sdk", "ndk", "25.1.893739");
        
        CMakeProjectAndroid testProj = new CMakeProjectAndroid("Dummy", Path, ndkPath);
        
        string buildDir = System.IO.Path.Combine(Path, "Generated", $@"build-{testProj.ProjectName}");
        string installPath = buildDir;
        
        CMakeBuildConfigurationAndroid testConf = new CMakeBuildConfigurationAndroid(buildDir, installPath, ThirdPartyPath, Path, testProj.BuildType, ndkPath);
        new CMakeBuildAndroid(testConf, testProj);

        var folder = new System.IO.DirectoryInfo(System.IO.Path.Combine(Path, System.IO.Path.Combine("Generated", $@"build-{testProj.ProjectName}")));
        if (folder.Exists)
            Assert.AreNotEqual(folder.GetFileSystemInfos().Length, 0);
        else
            Assert.AreEqual(true, false);
    }

    /*
    * Utility Function for getting root directory
    * Reference: https://stackoverflow.com/a/35824406
    */
    private static class VisualStudioProvider
    {
        public static System.IO.DirectoryInfo TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new System.IO.DirectoryInfo(
                currentPath ?? System.IO.Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
                directory = directory.Parent;
            if (directory != null)
                return directory;
            return new System.IO.DirectoryInfo("");
        }
    }
}