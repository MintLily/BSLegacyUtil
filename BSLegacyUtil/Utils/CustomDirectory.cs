namespace BSLegacyUtil.Utils; 

public static class CustomDirectory {
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = false) {
        // Get the subdirectories for the specified directory.
        var dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists) 
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDirName}");

        var dirs = dir.GetDirectories();

        // If the destination directory doesn't exist, create it.       
        Directory.CreateDirectory(destDirName);

        // Get the files in the directory and copy them to the new location.
        var files = dir.GetFiles();
        foreach (var file in files) {
            var tempPath = Path.Combine(destDirName, file.Name);
            file.CopyTo(tempPath, false);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (!copySubDirs) return;
        foreach (var subdir in dirs) {
            var tempPath = Path.Combine(destDirName, subdir.Name);
            DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
        }
    }
}