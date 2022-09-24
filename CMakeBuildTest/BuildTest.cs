namespace CMakeBuildTest;
using CMakeUnreal;

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
        CMakeProject testProj = new CMakeProject("Dummy", Path);
        new CMakeBuild(testProj, ThirdPartyPath, Path);

        var folder = new System.IO.DirectoryInfo(Path + $@"\\Generated\\build-{testProj.ProjectName}");
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