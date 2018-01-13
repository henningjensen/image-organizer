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
                ProcessDirectory(directory, output);
            }
        }

        public void ProcessFilesInDirectory(DirectoryInfo input, DirectoryInfo output)
        {
            foreach (FileInfo file in input.EnumerateFiles())
            {

                var imageFile = new ImageFile(file);

                if (!imageFile.ShouldBeProcessed())
                    continue;

                try
                {
                    imageFile.CopyTo(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while processing: " + file.Name + ", error message: " + e.Message);
                }
            }
        }

    }
}