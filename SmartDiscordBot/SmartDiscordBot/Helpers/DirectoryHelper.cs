using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartDiscordBot.Helpers
{
    public class DirectoryHelper
    {
        public static string GetFileDirectory(string fileName)
        {
            var filesParts = fileName.Split('.');
            var fileExtension = filesParts[filesParts.Length - 1];
            var targetDirectory = Directory.GetCurrentDirectory();

            var rootFiles = GetFilesFrom(targetDirectory, fileExtension);

            if (IsFileExistIn(rootFiles, fileName))
                return targetDirectory;

            var directories = Directory.GetDirectories(Directory.GetCurrentDirectory());

            foreach (var directory in directories)
            {
                var files = GetFilesFrom(directory, fileExtension);

                if (IsFileExistIn(files, fileName))
                    return directory;
            }

            return string.Empty;
        }

        private static bool IsFileExistIn(string[] files, string fileName)
        {
            foreach (var file in files)
            {
                if (file.EndsWith(fileName))
                {
                    return true;
                }
            }

            return false;
        }

        private static string[] GetFilesFrom(string directoryName, string extension)
        {
            return Directory.GetFiles(directoryName).Where(s => s.EndsWith(extension)).ToArray();
        }
    }
}
