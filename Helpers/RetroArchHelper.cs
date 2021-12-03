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

		//TODO: See how to block a second instance of RetroArch
		public static async Task<bool> IsRunning()
		{
			return Task.Run(() => Process.GetProcessesByName("retroarch.exe").FirstOrDefault()).Result != null;
		}

		public static async Task ListenForChanges(Action onCloseCallback)
		{
			Process retroArchProccess = Process.GetProcessesByName("retroarch.exe").FirstOrDefault();

			await Task.Run(() => retroArchProccess.WaitForExitAsync()).ContinueWith(t => 
			{
				onCloseCallback.Invoke();
			});
		}

		public static async Task ExecuteCommandWrapper(string command)
		{
			Process cmd = new Process();
			cmd.StartInfo.FileName = "powershell.exe";
			cmd.StartInfo.Arguments = command;
			cmd.StartInfo.CreateNoWindow = false;

			cmd.StartInfo.RedirectStandardOutput = true;

			cmd.Start();
		}

	}
}
