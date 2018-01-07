using System;
using System.IO;

namespace image_organizer
{
    public class FileManager
    {
        public void ProcessDirectory(DirectoryInfo input, DirectoryInfo output)
        {
            foreach (DirectoryInfo directory in input.EnumerateDirectories())
            {
                ProcessFilesInDirectory(input, output);
            }
        }

        public void ProcessFilesInDirectory(DirectoryInfo input, DirectoryInfo output)
        {
            foreach (FileInfo file in input.EnumerateFiles())
            {
                if (file.Extension != ".jpg")
                    continue;

                var metadata = new ImageMetadata(file);

                DateTime? dateTaken = metadata.DateTaken();

                if (dateTaken == null)
                    continue;

                int year = dateTaken.Value.Year;
                int month = dateTaken.Value.Month;

                DirectoryInfo destinationDirectory = output.CreateSubdirectory(year + "" + Path.DirectorySeparatorChar + month);
                string finalDestination = Path.Combine(destinationDirectory.FullName, file.Name);

                Console.WriteLine($"copying {file.FullName} to {finalDestination}");

                try
                {
                    file.CopyTo(finalDestination);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot copy file: " + file.Name + " due to error: " + e.Message);
                }

            }
        }

    }
}