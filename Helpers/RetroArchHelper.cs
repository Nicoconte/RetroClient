using SevenZipExtractor;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RetroClient.Helpers
{
    public class RetroArchHelper
    {
        public static List<string> UnallowedExtension = new List<string>()
        {
            ".txt", "txt", ".nfo", "nfo",".sfv", "sfv",
            ".ecm", "ecm", ".rar", "rar"
        };

        public static List<string> AllowedGameExtensions = new List<string>()
        {
            ".iso", "iso", ".bin", "bin", ".cue", "cue", ".n64",
            ".gba", "gba", ".sfc", "sfc"
        };

        private static void _NormalizeNamesFromDir(string path)
        {
            var files = Directory.GetFiles(path).ToList();

            if (!_NeedNormalization(files))
                return;

            files.ForEach(f =>
            {
                File.Move(f, $"{path}{Path.GetFileName(f).Replace("'", "")}");
                File.Delete(f);
            });
        }

        private static bool _NeedNormalization(List<string> files)
        {
            return files.Any(f => f.Contains("'"));
        }

        public static async Task ExtractFromZip(string filePath, string outputPath)
        {
            try
            {
                //Extract games
                ZipFile.ExtractToDirectory(filePath, outputPath);

                _NormalizeNamesFromDir(outputPath);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error -> ExtractFromZip -> { ex.Message }");
            }

        }

        public static async Task ExtractFrom7z(string filePath, string outputPath)
        {
            using (ArchiveFile archiveFile = new ArchiveFile(filePath))
            {
                archiveFile.Extract(outputPath); // extract all

                _NormalizeNamesFromDir(outputPath); // Ripoff all the single quotes from file name
            }
        }

        public static async Task ExtractFromRar(string filePath, string outputPath)
        {
            using (var archive = RarArchive.Open(filePath))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(outputPath, new ExtractionOptions()
                    {
                        ExtractFullPath = false,
                        Overwrite = true
                    });
                }
            }
        }

        public static async Task ProcessListener(Action onCloseCallback = null)
        {

            Thread listenerThread = new Thread(async () =>
            {

                bool isRunning = true;

                Process retroArchProcess = null;

                //Listen for a retroarch instance
                while (retroArchProcess == null)
                {
                    retroArchProcess = Process.GetProcessesByName("retroarch").FirstOrDefault();
                    await Task.Delay(1000);
                }

                Console.WriteLine("Proceso " + retroArchProcess.ProcessName);


                // We hook to the process.
                while (isRunning)
                {
                    Console.WriteLine("Running ma boi");
                    isRunning = !retroArchProcess.HasExited;
                    await Task.Delay(1000);
                }

                Console.WriteLine("Stopped");

                Console.WriteLine("Entering into Callback");

                if (onCloseCallback != null)
                {
                    onCloseCallback.Invoke();
                }
            });

            listenerThread.Start();

        }

        public static async Task ExecuteCommandWrapper(string command, Action onCloseCallback = null)
        {

            Process process = new Process();
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.CreateNoWindow = false;

            process.Start();

            Console.WriteLine("Starting Retro...");

            if (onCloseCallback != null)
            {
                onCloseCallback.Invoke();
            }

        }

    }
}
