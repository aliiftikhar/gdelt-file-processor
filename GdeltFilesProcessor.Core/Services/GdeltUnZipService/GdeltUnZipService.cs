using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace GdeltFilesProcessor.Core.Services.GdeltUnZipService
{
    public class GdeltUnZipService : IGdeltUnZipService
    {
        public Stream UnZip(Stream stream)
        {
            Stream unzippedStream = null;

            ZipArchive archive = new ZipArchive(stream);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    unzippedStream = entry.Open();
                    break;
                }
            }

            return unzippedStream;
        }
    }
}
