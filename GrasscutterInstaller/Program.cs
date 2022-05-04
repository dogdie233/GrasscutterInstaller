using GrasscutterInstaller;
using GrasscutterInstaller.Properties;

using NLog;

using System.IO.Compression;

const string ServerPath = "Server";
const string TempPath = "Temp";

// Configurate logger output
#if DEBUG
LogManager.Configuration.Variables["consoleMinLevel"] = "Trace";
#endif
ILogger logger = LogManager.GetLogger("Crasscutter Installer");

logger.Info(ProgramText.ApplicationRun);

logger.Info(string.Format(ProgramText.GetRepository, "Grasscutter"));
var gcRepo = await GithubRepository.GetFromGithubAsync("Grasscutters", "Grasscutter");
logger.Info("Grasscutter: " + gcRepo.data.Description);
logger.Info(string.Format(ProgramText.GCLicenseKey, gcRepo.data));

logger.Info(string.Format(ProgramText.GetBranches, "Grasscutter"));
var branchNames = gcRepo.Branches.Value.Select(b => b.Name).ToArray();
logger.Debug($"Branches: {string.Join(", ", branchNames)}");
Console.Write(string.Format(ProgramText.SelectBranch, string.Join(", ", branchNames)));
var selectBranchName = Console.ReadLine();
if (selectBranchName == null)
{
    logger.Fatal(ProgramText.BranchInputNull);
    Environment.Exit(1);
}
var selectBranch = gcRepo.Branches.Value.Where(b => b.Name == selectBranchName).FirstOrDefault();
if (selectBranch == null)
{
    logger.Fatal(string.Format(ProgramText.BranchInputNotFound, selectBranchName));
    Environment.Exit(1);
}

using var downloadGCZipProgress = new ConsoleProgressBar(string.Format(ProgramText.DownloadTaskName, "GrassCutter"), logger);
using var gcZipStream = new MemoryStream();
try
{
    NetworkHelper.DownloadAsync(gcZipStream, $"https://github.com/Grasscutters/Grasscutter/archive/refs/heads/{selectBranch.Name}.zip", downloadGCZipProgress).Wait();
}
catch (Exception e)
{
    logger.Fatal(e, string.Format(ProgramText.DownloadTaskName, "Grasscutter"));
    Environment.Exit(1);
}
finally
{
    downloadGCZipProgress.Dispose();
}

if (!Directory.Exists(ServerPath)) { Directory.CreateDirectory(ServerPath); }
using var extractGCZipProgress = new ConsoleProgressBar(string.Format(ProgramText.UnzipTaskName, "Grasscutter", ServerPath), logger);
// logger.Info(string.Format(ProgramText.UnzipTaskName, "Grasscutter", ServerPath));
var gcZipArchive = new ZipArchive(gcZipStream);
gcZipArchive.ExtractToDirectorySmart(ServerPath, extractGCZipProgress);

extractGCZipProgress.Dispose();
gcZipArchive.Dispose();
gcZipStream.Dispose();

using var downloadGDZipProgress = new ConsoleProgressBar(string.Format(ProgramText.DownloadTaskName, "GenshinData"), logger);
using var gdZipStream = new MemoryStream();
try
{
    NetworkHelper.DownloadAsync(gdZipStream, "https://github.com/Dimbreath/GenshinData/archive/refs/heads/master.zip", downloadGDZipProgress).Wait();
}
catch (Exception e)
{
    logger.Fatal(e, string.Format(ProgramText.DownloadTaskName, "GenshiData"));
    Environment.Exit(1);
}
finally
{
    downloadGDZipProgress.Dispose();
}

var resourcePath = ServerPath + "/resources";
if (!Directory.Exists(resourcePath)) { Directory.CreateDirectory(resourcePath); }
using var extractGDZipProgress = new ConsoleProgressBar(string.Format(ProgramText.UnzipTaskName, "GenshinData", ServerPath), logger);
// logger.Info(string.Format(ProgramText.UnzipTaskName, "GenshinData", resourcePath));
var gdZipArchive = new ZipArchive(gdZipStream);
gdZipArchive.ExtractToDirectorySmart(resourcePath, extractGDZipProgress);

extractGDZipProgress.Dispose();
gdZipArchive.Dispose();
gdZipStream.Dispose();
