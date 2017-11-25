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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var p = new FileInfo(args[0]).FullName;

            var metadata = new ImageMetadata(args[0]);

            Console.WriteLine("Caption: " + metadata.Caption());
            Console.WriteLine("Directory: " + metadata.DirectoryName());
            
/*
            IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(p);
            var subIfdDirectory = directories.OfType<MetadataExtractor.Formats.Iptc.IptcDirectory>().FirstOrDefault();
            var dateTime = subIfdDirectory?.GetDescription(MetadataExtractor.Formats.Iptc.IptcDirectory.TagCategory);
            Console.WriteLine(dateTime);

            var exifReader = new ExifLib.ExifReader(p);
            
            //Double[] latitude;
            exifReader.GetTagValue<Double[]>(ExifLib.ExifTags.GPSLatitude, out Double[] latitude);

            // Double[] longitude;
            exifReader.GetTagValue<Double[]>(ExifLib.ExifTags.GPSLongitude, out Double[] longitude);

            

            Console.WriteLine(p);

            if (latitude != null)
                Console.WriteLine($"Lat: {latitude[0]},{latitude[1]}");
            if (longitude != null)
                Console.WriteLine($"Lon: {longitude[0]},{longitude[1]}");

                 */

        }



    }
}
