using System.Collections.Generic;
using System.IO;

namespace IndentationFixer
{
    public static class FileCleanup
    {
        public static void ReadFileLineByLine(FileInfo file)
        {
            var fileLines = new List<string>();
            var fileWasChanged = false;
            foreach (var fileLine in File.ReadLines(file.FullName))
            {
                var lineCleanup = new LineCleanup(fileLine);
                lineCleanup.CleanEmptyLines();
                lineCleanup.RemoveEndSpaces();
                lineCleanup.ReplaceTabToSpaces();
                fileWasChanged |= lineCleanup.IsChanged;
                fileLines.Add(lineCleanup.InternalString);
            }

            if (fileWasChanged)
            {
                File.WriteAllLines(file.FullName, fileLines);
            }
        }
    }
}