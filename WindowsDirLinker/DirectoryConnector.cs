using System;
using System.Diagnostics;
using System.IO;

namespace WindowsDirLinker
{
    internal class DirectoryConnector : IDirectoryConnector
    {
        public string PathToCurrentDir { get; set; }
        public string PathToRealDir { get; set; }
        public string FakeDirName { get; set; }

        private DirectoryInfo currentDir;
        private DirectoryInfo realDir;

        public void Connect()
        {
            CheckNullValues();

            currentDir = GetDirIfExist(PathToCurrentDir);
            realDir = GetDirIfExist(PathToRealDir);

            ConnectViaCmd();
        }

        private void CheckNullValues()
        {
            if (PathToCurrentDir == null)
                throw new ArgumentNullException(nameof(PathToCurrentDir));

            if (PathToRealDir == null)
                throw new ArgumentNullException(nameof(PathToRealDir));
        }

        private DirectoryInfo GetDirIfExist(string path)
        {
            var dir = new DirectoryInfo(path);

            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            return dir;
        }

        private string GetFakeDirName() 
            => string.IsNullOrWhiteSpace(FakeDirName) ? realDir.Name : FakeDirName;

        private void ConnectViaCmd()
#if DEBUG 
            => Process.Start("cmd", $"/K MKLINK /J \"{currentDir.FullName}\\{GetFakeDirName()}\" \"{realDir.FullName}\"");
#else
            => Process.Start("cmd", $"/C MKLINK /J \"{currentDir.FullName}\\{GetFakeDirName()}\" \"{realDir.FullName}\"");
#endif
    }
}
