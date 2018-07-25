using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Sync
{
    public class PathInfo
    {
        public PathInfo(string dir, string file)
        {
            Directory = dir;
            File = file;
        }

        public string Directory { get; set; }
        public string File { get; set; }

        public string FullPath => Path.GetFullPath(Path.Combine(Directory, File));

        public static bool operator ==(PathInfo p1, PathInfo p2)
        {
            var hash1 = ComputeHash(p1);
            var hash2 = ComputeHash(p2);

            return hash1 == hash2;
        }

        private static string ComputeHash(PathInfo p1)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(p1.FullPath))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public override bool Equals(object obj)
        {
            var info = obj as PathInfo;
            return info != null &&
                   Directory == info.Directory &&
                   File == info.File &&
                   FullPath == info.FullPath;
        }

        public override int GetHashCode()
        {
            var hashCode = -1396477410;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Directory);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(File);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullPath);
            return hashCode;
        }

        public static bool operator !=(PathInfo p1, PathInfo p2)
        {
            return !(p1 == p2);
        }
    }
}
