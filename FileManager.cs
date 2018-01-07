using System;
using System.IO;

namespace image_organizer
{
    public class FileManager
    {
        public void ProcessDirectory(DirectoryInfo input, DirectoryInfo output)
        {
            ProcessFilesInDirectory(input, output);

            foreach (DirectoryInfo directory in input.EnumerateDirectories())
            {
                ProcessFilesInDirectory(input, output);
            }
        }

        public void ProcessFilesInDirectory(DirectoryInfo input, DirectoryInfo output)
        {
            foreach (FileInfo file in input.EnumerateFiles())
            {
                var fileExtension = file.Extension.ToLower();
                if (fileExtension != ".jpg")
                    continue;

                var metadata = new ImageMetadata(file);

                DateTime? dateTaken = metadata.DateTaken();

                if (dateTaken == null)
                    continue;

                string year = dateTaken.Value.ToString("yyyy");
                string month = dateTaken.Value.ToString("MM");

                DirectoryInfo destinationDirectory = output.CreateSubdirectory(year + "" + Path.DirectorySeparatorChar + month);
                
                string outputFileName = dateTaken.Value.ToString("yyyy-MM-dd_HHmmss");

                string finalDestination = Path.Combine(destinationDirectory.FullName, outputFileName + fileExtension);

                // TODO: Skip existing files with same file size

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