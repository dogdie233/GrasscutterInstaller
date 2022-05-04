using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrasscutterInstaller;

public static class Utils
{
    private const string LogPath = "logs";
    private static readonly DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);

    public static void DumpExceptionJson(string jsonText, Type targetType, HttpResponseMessage responseMessage)
    {
        var time = DateTime.Now - startTime;
        var path = LogPath + "/ErrorJsonDump-" + time.TotalMilliseconds + ".json";
        if (!Directory.Exists(LogPath)) { Directory.CreateDirectory(LogPath); }
        using var fs = File.Create(LogPath + path);
        using var writer = new StreamWriter(fs, Encoding.UTF8);
        writer.WriteLine("Time: " + time.ToString());
        if (responseMessage.RequestMessage != null)
        {
            writer.WriteLine("Uri: " + responseMessage.RequestMessage.RequestUri);
            writer.WriteLine("Request Headers: ");
            foreach (var header in responseMessage.RequestMessage.Headers)
            {
                writer.Write(" - ");
                writer.Write(header.Key);
                writer.Write(": ");
                writer.WriteLine(string.Join(' ', header.Value));
            }
        }
        writer.WriteLine("Target Type: " + targetType.Name.ToString());
        writer.WriteLine("Responded json text: " + jsonText);
        writer.Flush();
    }

    public static void ExtractToDirectorySmart(this ZipArchive zipArchive, string path, IProgress<double>? progress)
    {
        if (zipArchive.Entries.Count == 0) { return; }
        path = path.Replace('\\', '/');
        path = path.EndsWith('/') ? path.Substring(0, path.Length - 1) : path;
        var entryNames = zipArchive.Entries.Select(entry => entry.FullName.Replace('\\', '/')).ToArray();
        var folderName = entryNames[0].Substring(0, entryNames[0].IndexOf('/'));

        var index = 0;
        bool aFolder = false;
        foreach (var name in entryNames)
        {
            if (name.Substring(0, name.IndexOf('/')) != folderName)
            {
                aFolder = true;
                return;
            }
        }
        foreach (var entry in zipArchive.Entries)
        {
            var destName = entry.FullName.Replace('\\', '/');
            if (aFolder) { destName = destName.Remove(0, folderName.Length + 1); }
            var destPath = path + '/' + destName;

            if (string.IsNullOrEmpty(destName)) { continue; }
            if (destName.EndsWith('/')) { Directory.CreateDirectory(path + '/' + destName); }
            else { entry.ExtractToFile(path + '/' + destName); }
            index++;
            progress?.Report((double)index / zipArchive.Entries.Count);
        }
    }
}
