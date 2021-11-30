using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Helpers
{
	public class FileHelper
	{
		public static void ReplaceFilename(string oldName, string newName)
		{
			File.Move(oldName, newName);
			File.Delete(oldName);
		}

		public static void MoveFiles(List<string> filesPath, string newPath)
		{
			try 
			{
				foreach (var path in filesPath)
				{
					string fullNewPath = $"{newPath}/{Path.GetFileName(path)}";

					File.Move(path, fullNewPath);

					File.Delete(path);
				}
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
