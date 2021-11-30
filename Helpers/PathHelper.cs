using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RetroClient.Helpers
{
	public class PathHelper
	{
		public static string ROOT_PATH = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string OUTPUT_PATH = ROOT_PATH + "/Output";
    }

}
