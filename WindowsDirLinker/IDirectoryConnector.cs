namespace WindowsDirLinker
{
    internal interface IDirectoryConnector
    {
        string PathToCurrentDir { get; set; }
        string PathToRealDir { get; set; }
        string FakeDirName { get; set; }

        void Connect();
    }
}