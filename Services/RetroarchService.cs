using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Compression;
using System.IO;

namespace RetroClient.Services
{
	public class RetroarchService
	{

		public async Task StartGame(Setting setting, string core, string game)
		{
			try
			{

				//var tempPath = "D:/GIT/_me/RetroClient/bin/Debug/net5.0/win-x64/Output/TempDir/";

				var gamePath = $"{setting.GamesPath}{game}";

				//Directory.GetFiles(tempPath).ToList().ForEach(p => File.Delete(p));


				//ZipFile.ExtractToDirectory(gamePath, tempPath);

				//var filesFromTemp = Directory.GetFiles(tempPath);

				//var flatFiles = string.Join(' ', filesFromTemp.Where(f => !f.EndsWith(".txt")));

				var command = $"{setting.RetroArchPath}retroarch.exe --verbose -L '{setting.RetroArchCorePath}{core}' '{gamePath}'";

				Console.WriteLine(command);

				Process cmd = new Process();
				cmd.StartInfo.FileName = "powershell.exe";
				cmd.StartInfo.Arguments = command;
				cmd.StartInfo.CreateNoWindow = false;

				cmd.StartInfo.RedirectStandardOutput = true;

				cmd.Start();

				string output = cmd.StandardOutput.ReadToEnd();

				Console.WriteLine(output);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.InnerException);
				Console.WriteLine(ex.StackTrace);
			}

		}
	}
}
