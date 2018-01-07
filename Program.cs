using System;
using System.Collections.Generic;
using System.IO;
using MetadataExtractor;
using System.Linq;
using MetadataExtractor.Formats.Exif;

namespace image_organizer
{
    class Program
    {
        static int Main(string[] args)
        {
            /*
            if (args == null || args.Length == 0 || string.IsNullOrWhiteSpace(args[0])) {
                Console.WriteLine("No file name given. Please provide a file name.");
                return (int)ExitCode.InvalidFilename;
            }

            var fileInfo = new FileInfo(args[0]);

            if (!fileInfo.Exists) {
                Console.WriteLine("Invalid file name, cannot find file.");
                return (int)ExitCode.InvalidFilename;
            }

            var metadata = new ImageMetadata(fileInfo);

            Console.WriteLine("Caption: " + metadata.Caption());
            Console.WriteLine("Directory: " + metadata.DirectoryName());
            Console.WriteLine("DateTaken: " + metadata.DateTaken());
 */
            Console.WriteLine("input dir: " + args[0]);
            Console.WriteLine("output dir: " + args[1]);

            new FileManager().ProcessDirectory(new DirectoryInfo(args[0]), new DirectoryInfo(args[1]));

            return (int)ExitCode.Success;
        }
    }

    enum ExitCode : int
    {
        Success = 0,
        InvalidFilename = 2,
        UnknownError = 10
    }
}
