using Microsoft.EntityFrameworkCore;
using RetroClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroClient.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<VideoGame> VideoGames { get; set; }
		public DbSet<VideoGamePlatform> Platforms { get; set; }
	}
}
