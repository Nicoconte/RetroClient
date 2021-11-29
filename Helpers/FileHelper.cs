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
	}
}
