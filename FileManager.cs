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
                var fileExtension = file.Extension.ToLower();
                if (fileExtension != ".jpg")
                    continue;

                try
                {
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

                    var destinationFile = new FileInfo(finalDestination);
                    if (destinationFile.Exists)
                    {
                        if (destinationFile.Length == file.Length)
                        {
                            Console.WriteLine("Skipping file, already exists: " + finalDestination);
                            continue;
                        }
                        else
                        {
                            // same date, but different size
                            int counter = 0;
                            do 
                            {
                                counter++;
                                string fileName = outputFileName + "_" + counter.ToString("D2") + fileExtension;
                                finalDestination = Path.Combine(destinationDirectory.FullName, fileName);
                            }
                            while(new FileInfo(finalDestination).Exists);
                            
                        }
                    }

                    Console.WriteLine($"Copying {file.FullName} to {finalDestination}");

                    file.CopyTo(finalDestination);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while processing: " + file.Name + ", error message: " + e.Message);
                }
            }
        }

    }
}