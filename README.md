[![Build status](https://ci.appveyor.com/api/projects/status/j0hl6o56ob0dq22l?svg=true)](https://ci.appveyor.com/project/henningjensen/image-organizer)

ImageOrganizer
====

What does it do?
--

1. Locates all jpg files in given directory (recursively)
1. For each file
1. Rename file according to DateTaken, e.g. yyyy-MM-dd_HHmmss.jpg
1. Copy to a folder structure based on year/month.


The final output directory structure will look like this:

    2018/01/2018-01-01_000100.jpg
    2017/12/2018-12-22_123400.jpg
    2017/12/2018-12-21_102200.jpg

Usage
--

    $ dotnet run image-organizer.dll inputdirectory outputdirectory

The original files will not be modified. All files are copied from input to output directory.

Features
--
* Duplicates are skipped
* Images with same DateTaken tag are put in same directory but suffixed with an unique number.

Download
--

Dotnet runtime is required to run. Dotnet SDK is required to build it. Download it from https://www.microsoft.com/net/download/

Download source code and build it (sdk ) with

    $ dotnet build

Or download compiled dll from AppVeyour: https://ci.appveyor.com/project/henningjensen/image-organizer/build/artifacts
