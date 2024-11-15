namespace AdventOfCode.Day07
{
    class Folder
    {
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }

        public List<File> Files { get; set; } = [];

        public int Level { get; set; }
        public string ParentFolderName { get; set; } = string.Empty;
        public List<Folder> SubFolders { get; set; } = [];
        public Folder? ParentFolder { get; set; }
    }

    class File
    {
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
    }
}