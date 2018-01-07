using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExifLib;
using MetadataExtractor;

class ImageMetadata
{
    FileInfo _filePath;
    ExifReader _exifReader;
    IEnumerable<MetadataExtractor.Directory> _directories;

    public ImageMetadata(FileInfo filePath)
    {
        _filePath = filePath;
        _exifReader = new ExifReader(_filePath.FullName);
        _directories = ImageMetadataReader.ReadMetadata(_filePath.FullName);
        
    }

    public string Caption() {
        var subIfdDirectory = _directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>().FirstOrDefault();
        var userComment = subIfdDirectory?.GetDescription(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagUserComment);
        return userComment;
    }
    
    public string DirectoryName() {
        return _filePath.Directory.Name;
    }

    public DateTime? DateTaken() {
        DateTime? dateTaken = null;
        if (GetExifSubIfdDirectory() != null) {
            if (GetExifSubIfdDirectory().ContainsTag(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagDateTimeOriginal)) {
                dateTaken = GetExifSubIfdDirectory().GetDateTime(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagDateTimeOriginal);
            }
        }
        return dateTaken;
    }

    private MetadataExtractor.Directory GetExifSubIfdDirectory() {
        return _directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>()?.FirstOrDefault();
    }

}