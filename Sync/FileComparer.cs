using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sync
{
    public class FileComparer : IEqualityComparer<PathInfo>
    {
        public bool Equals(PathInfo x, PathInfo y)
        {
            return x == y;
        }

        public int GetHashCode(PathInfo obj)
        {
            unchecked
            {
                int hash = 13;
                hash = hash * 17 + obj.File.GetHashCode();
                return hash;
            }
        }
    }
}
