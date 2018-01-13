using System;
using System.IO;

namespace image_organizer
{
    public class ImageFile
    {
        private FileInfo _originalFileInfo;
        private ImageMetadata _metadata;

        public ImageFile(FileInfo fileInfo)
        {
            _originalFileInfo = fileInfo;
            _metadata = ImageMetadata.GetMetadata(_originalFileInfo);
        }

        public void CopyTo(DirectoryInfo destinationBaseDirectory)
        {
            DirectoryInfo destinationDirectory = CreateDestinationDirectory(destinationBaseDirectory);

            FileInfo destinationFileName = CreateDestinationFileName(destinationDirectory);
            WriteFileTo(destinationFileName);
        }

        private void WriteFileTo(FileInfo incomingDestination)
        {
            FileInfo destination = incomingDestination;

            if (destination.Exists)
            {
                if (destination.Length == _originalFileInfo.Length)
                {
                    Console.WriteLine("Skipping file, already exists: " + destination);
                    return;
                }
                else
                {
                    destination = CreateFileNameForDuplicateFile(destination);
                    if (destination == null)
                        return;
                }
            }

            Console.WriteLine($"Copying {_originalFileInfo.FullName} to {destination}");

            _originalFileInfo.CopyTo(destination.FullName);
        }

        private FileInfo CreateFileNameForDuplicateFile(FileInfo destination)
        {
            int counter = 0;

            while (destination.Exists) {
                if (_originalFileInfo.Length == destination.Length) {
                    Console.WriteLine("Duplicate file, already exists: " + destination.FullName);
                    return null;
                }
                counter++;
                string fileName = DateTakenFormatted() + "_" + counter.ToString("D2") + GetExtension();
                destination = new FileInfo(Path.Combine(destination.DirectoryName, fileName));
            } 

            return destination;
        }

        private FileInfo CreateDestinationFileName(DirectoryInfo destinationDirectory)
        {
            string destinationFileName = Path.Combine(destinationDirectory.FullName, DateTakenFormatted() + GetExtension());
            return new FileInfo(destinationFileName);
        }

        private DirectoryInfo CreateDestinationDirectory(DirectoryInfo destinationBaseDirectory)
        {
            DateTime dateTaken = _metadata.DateTaken().Value;
            string year = dateTaken.ToString("yyyy");
            string month = dateTaken.ToString("MM");

            return destinationBaseDirectory.CreateSubdirectory(year + Path.DirectorySeparatorChar + month);
        }

        private string DateTakenFormatted()
        {
            return _metadata.DateTaken().Value.ToString("yyyy-MM-dd_HHmmss");
        }

        public bool ShouldBeProcessed()
        {
            return IsImage() && _metadata != null && _metadata.HasDateTaken();
        }

        private bool IsImage()
        {
            return GetExtension() == ".jpg";
        }

        private string GetExtension()
        {
            return _originalFileInfo.Extension.ToLower();
        }


    }
}