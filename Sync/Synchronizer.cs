using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sync
{
    public class Synchronizer
    {
        private readonly string FolderA;
        private readonly string FolderB;

        private List<PathInfo> folderAInfo = new List<PathInfo>();
        private List<PathInfo> folderBInfo = new List<PathInfo>();

        public Synchronizer(string folderA, string folderB)
        {
            FolderA = folderA;
            FolderB = folderB;
        }

        public void Sync()
        {
            folderAInfo = ParseFolder(FolderA);
            folderBInfo = ParseFolder(FolderB);

            var intersect = folderAInfo.Intersect(folderBInfo, new FileComparer()).ToList();

            ReduceFolderInfo(intersect, folderAInfo);
            ReduceFolderInfo(intersect, folderBInfo);

        }

        private void ReduceFolderInfo(List<PathInfo> intersect, List<PathInfo> folderInfo)
        {
            Console.WriteLine($"Found {intersect.Count} files in common");
            if (intersect.Any())
            {
                Console.WriteLine("Optimizing folder info...");
                foreach (var item in intersect)
                    folderInfo.RemoveAll(x => x.File == item.File);
            }

        }

        private List<PathInfo> ParseFolder(string folderPath)
        {
            Console.WriteLine($"Processing {folderPath}...");
            var isPathValid = Directory.Exists(folderPath);
            var folderInfo = new List<PathInfo>();

            if (isPathValid)
            {
                var directories = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories).ToList();
               directories.Add(folderPath);
                foreach (var dir in directories)
                {
                    var files = Directory.GetFiles(dir);
                    foreach (var file in files)
                    {
                        var info = new PathInfo(dir, Path.GetFileName(file));
                        Console.WriteLine($"adding {info.FullPath}");
                        folderInfo.Add(info);
                    }
                }
            }

            return folderInfo;

        }
    }
}
