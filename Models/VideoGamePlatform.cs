using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Models
{
	public class VideoGamePlatform
	{
		[Key]
		public string Id { get; set; }
		public string PlatformName { get; set; }
		public string CreatedAt { get; set; } = DateTime.UtcNow.AddHours(-3).ToString();
	}
}
