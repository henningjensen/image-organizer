using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExifLib;
using MetadataExtractor;

namespace image_organizer
{
    public class ImageMetadata
    {
        FileInfo _filePath;
        ExifReader _exifReader;
        IEnumerable<MetadataExtractor.Directory> _directories;

        public static ImageMetadata GetMetadata(FileInfo filePath)
        {
            try
            {
                IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(filePath.FullName);
                return new ImageMetadata(filePath, directories);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while trying to fetch metadata for " + filePath.FullName + ": " + e.Message);
                return null;
            }
        }

        private ImageMetadata(FileInfo filePath, IEnumerable<MetadataExtractor.Directory> directories)
        {
            _filePath = filePath;
            _exifReader = new ExifReader(_filePath.FullName);
            _directories = directories;
        }

        public string Caption()
        {
            var subIfdDirectory = _directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>().FirstOrDefault();
            var userComment = subIfdDirectory?.GetDescription(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagUserComment);
            return userComment;
        }

        public string DirectoryName()
        {
            return _filePath.Directory.Name;
        }

        public bool HasDateTaken()
        {
            return DateTaken() != null;
        }

        public DateTime? DateTaken()
        {
            DateTime? dateTaken = null;
            if (GetExifSubIfdDirectory() != null)
            {
                if (GetExifSubIfdDirectory().ContainsTag(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagDateTimeOriginal))
                {
                    dateTaken = GetExifSubIfdDirectory().GetDateTime(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagDateTimeOriginal);
                }
            }
            return dateTaken;
        }

        private MetadataExtractor.Directory GetExifSubIfdDirectory()
        {
            return _directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>()?.FirstOrDefault();
        }

    }
}