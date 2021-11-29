using RetroClient.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Models
{
	public class Setting
	{
		[Key]
		public string Id { get; set; }

		public string GamesPath { get; set; }
		
		public string RetroArchPath { get; set; }
		
		public string RetroArchCorePath { get; set; }
	}
}
