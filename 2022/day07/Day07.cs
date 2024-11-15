using System.Text.RegularExpressions;

namespace AdventOfCode.Day07
{
    class Day07 : DailyTask
    {
        List<Folder> _folderStructure = [];
        Dictionary<string, int> _folderFileSize = [];
        readonly string _commandMoveDirectoryPattern = @"^\$ cd\s+(.+)$";
        readonly string _commandListDirectoryPattern = @"\$\s*ls\s*";

        public override string PartOne()
        {
            _folderStructure = GetFolderStructure();
            Console.WriteLine("Folderstructure ✅");

            _folderStructure.ForEach(x => GetFilesSize(x));

            Console.WriteLine("Folder files' size ✅");
            var totalSum = _folderFileSize.Where(x => x.Value <= 100000).Sum(x => x.Value);

            return totalSum.ToString();
        }

        public override string PartTwo()
        {
            var deletedData = 0;
            var minReqDeletedData = 0;
            var totalDiscSpace = 70000000;
            var minDiscSpace = 30000000;
            var outerMostKey = "/-";

            _folderStructure = GetFolderStructure();
            _folderStructure.ForEach(x => GetFilesSize(x));

            var usedDiscSpace = _folderFileSize[outerMostKey];
            var unusedDiscScpace = totalDiscSpace - usedDiscSpace;

            if (unusedDiscScpace < minDiscSpace)
            {
                minReqDeletedData = minDiscSpace - unusedDiscScpace;
            }

            deletedData = _folderFileSize.Where(x => x.Value >= minReqDeletedData).Select(x => x.Value).Order().First();
            return deletedData.ToString();
        }

        private List<Folder> GetFolderStructure()
        {
            var splittedInput = Input.Split("\n");
            var folders = new List<Folder>() { new() { Name = "/" } };

            Folder currentFolder = null!;

            int level = 0;

            for (var i = 0; i < splittedInput.Length; i++)
            {
                Match moveDirectoryMatch = Regex.Match(splittedInput[i], _commandMoveDirectoryPattern);
                Match listDirectoryMatch = Regex.Match(splittedInput[i], _commandListDirectoryPattern);

                if (moveDirectoryMatch.Success)
                {
                    string directory = moveDirectoryMatch.Groups[1].Value;
                    if (directory != "..")
                    {
                        level++;
                        if (currentFolder != null)
                            currentFolder = currentFolder.SubFolders.First(x => x.Name == directory);
                        else
                            currentFolder = folders.First();
                    }
                    else
                    {
                        level--;
                        currentFolder = currentFolder.ParentFolder;
                    }
                }
                else if (listDirectoryMatch.Success)
                {
                    var itemsInDirectory = new List<string>();
                    for (int j = i + 1; j < splittedInput.Length; j++)
                    {
                        if (Regex.IsMatch(splittedInput[j], _commandMoveDirectoryPattern) ||
                            Regex.IsMatch(splittedInput[j], _commandListDirectoryPattern))
                        {
                            i = j - 1;
                            break;
                        }
                        itemsInDirectory.Add(splittedInput[j]);
                    }

                    foreach (var itemInDirectory in itemsInDirectory)
                    {
                        var splittedString = itemInDirectory.Split(' ');
                        if (splittedString.Length == 2 && splittedString[0] != "dir")
                        {
                            var newFile = new File() { Size = int.Parse(splittedString[0]), Name = splittedString[1] };
                            currentFolder!.Files.Add(newFile);
                        }
                        else if (splittedString.Length == 2 && splittedString[0] == "dir")
                        {
                            if (!currentFolder!.SubFolders.Any(x => x.Name == splittedString[1]))
                            {
                                var newFolder = new Folder() { Name = splittedString[1], Level = level + 1, ParentFolder = currentFolder, ParentFolderName = currentFolder.Name };
                                currentFolder.SubFolders.Add(newFolder);
                            }
                        }
                    }
                }
            }
            return folders;
        }

        private int GetFilesSize(Folder folder)
        {
            var filesSize = folder.Files.Sum(f => f.Size);

            if (folder.SubFolders.Count > 0)
            {
                filesSize += folder.SubFolders.Select(x => GetFilesSize(x)).Sum();
                folder.SubFolders.ForEach(s => GetFilesSize(s));
            }

            if (_folderFileSize.TryGetValue($"{folder.Name}-{folder.ParentFolderName}", out int _))
            {
                _folderFileSize[$"{folder.Name}-{folder.ParentFolderName}"] = filesSize;
            }
            else
            {
                _folderFileSize.Add($"{folder.Name}-{folder.ParentFolderName}", filesSize);
            }

            return filesSize;
        }
    }
}
