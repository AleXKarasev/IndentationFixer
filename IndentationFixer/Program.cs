using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace IndentationFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ReadSettings();
            var fileList = GetAllFiles(settings.InputFolder, settings.FilesMask);

            var index = 0;
            foreach (var fileInfo in fileList)
            {
                FileCleanup.ReadFileLineByLine(fileInfo);
                Console.WriteLine($"{index++} file from {fileList.Count}. Process {fileInfo.FullName} ...");
            }
        }

        private static (string InputFolder, string FilesMask) ReadSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configurationRoot = builder.Build();
            return (configurationRoot["InputFolder"], configurationRoot["FilesMask"]);
        }

        private static IReadOnlyCollection<FileInfo> GetAllFiles(string path, string filesMask)
        {
            var dictionary = new DirectoryInfo(path);
            if (!dictionary.Exists)
            {
                throw new Exception("Input folder does not exist!");
            }

            return dictionary.GetFiles(filesMask, SearchOption.AllDirectories);
        }
    }
}