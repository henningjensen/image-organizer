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

    public ImageMetadata(FileInfo filePath)
    {
        _filePath = filePath;
        _exifReader = new ExifReader(_filePath.FullName);
    }

    public string Caption() {
        IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(_filePath.FullName);
        var subIfdDirectory = directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>().FirstOrDefault();
        var userComment = subIfdDirectory?.GetDescription(MetadataExtractor.Formats.Exif.ExifDirectoryBase.TagUserComment);
        return userComment;
    }
    
    public string DirectoryName() {
        return _filePath.Directory.Name;
    }

}