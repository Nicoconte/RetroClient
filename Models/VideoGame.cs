using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Models
{
	public class VideoGame
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Platform { get; set; }
		public string DownloadUrl { get; set; }
		public string SourceUrl { get; set; }
		public string ImageUrl { get; set; }
		public string CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-3).ToString();
	}
}
