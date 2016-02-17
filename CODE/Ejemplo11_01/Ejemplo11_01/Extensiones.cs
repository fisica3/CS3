using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using FindFiles;

namespace PlainConcepts.Linq
{
    public static partial class Extensiones
    {
        public static IEnumerable<FileInfo> GetFileInfos(
            this DirectoryInfo dir,
            string fileTypesToMatch, 
            bool includeSubDirs)
        {
            using (FileSystemEnumerator fse =
                 new FileSystemEnumerator(
                     dir.FullName,
                     fileTypesToMatch,
                     includeSubDirs))
            {
                foreach (FileInfo fi in fse.Matches())
                    yield return fi;
            }
        }
    }
}
