using Blazored.Modal;
using Blazored.Toast;
using Blazored.LocalStorage;

using ElectronNET.API;
using ElectronNET.API.Entities;

using RetroClient.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RetroClient
{
	public class Startup
	{
		private bool _showDevTools = false;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		private async void CreateWindow()
		{
			var window = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions() {
				Width = 1366,
				Height = 768,
				AutoHideMenuBar = true
			});

			if (_showDevTools)
			{
				window.WebContents.OpenDevTools();
			}

			window.OnClosed += () => {
				Electron.App.Quit();
			};
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddBlazoredModal();
			services.AddBlazoredToast();

			services.AddBlazoredLocalStorage(config =>
			{
				config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
				config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
				config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
				config.JsonSerializerOptions.WriteIndented = false;
			});

			services.AddDbContext<ApplicationDbContext>(options => {
				options.UseSqlite("Data source=RetroClientDB.db");
			});

			services.AddRazorPages();
			services.AddServerSideBlazor();

			if (HybridSupport.IsElectronActive)
			{
				Task.Run(() => CreateWindow());
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}
