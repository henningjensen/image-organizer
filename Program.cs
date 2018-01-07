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

            if (args == null || args.Length == 0
                || string.IsNullOrWhiteSpace(args[0])
                || string.IsNullOrWhiteSpace(args[1]))
            {
                Console.WriteLine("Missing input and/or output directory arguments.");
                Console.WriteLine("Usage: ImageOrganizer inputDir outputDir");
                return (int)ExitCode.InvalidDirectoryName;
            }

            Console.WriteLine("input dir: " + args[0]);
            Console.WriteLine("output dir: " + args[1]);

            var inputDir = new DirectoryInfo(args[0]);
            var outputDir = new DirectoryInfo(args[1]);
            if (!outputDir.Exists)
            {
                Console.WriteLine("Output directory did not exist. Creating it now.");
                outputDir.Create();
            }

            new FileManager().ProcessDirectory(inputDir, outputDir);

            return (int)ExitCode.Success;
        }
    }

    enum ExitCode : int
    {
        Success = 0,
        InvalidDirectoryName = 2,
        UnknownError = 10
    }
}
