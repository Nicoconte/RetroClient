using RetroClient.Models;
using RetroClient.Helpers;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace RetroClient.Services
{
    public class RetroArchService
    {

        public static async Task StartGame(Setting setting, string core, string game)
        {
            Console.WriteLine($"Meanwhile rendering");

            var outputTempPath = "D:/GIT/_me/RetroClient/bin/Debug/net5.0/win-x64/Output/TempDir/";
            var filesFromOutputPath = Directory.GetFiles(outputTempPath).ToList();

            filesFromOutputPath.ForEach(f =>
            {
                File.Delete(f);
            });

            var pathToGame = $"{setting.GamesPath}{game}";
            var gameExtension = Path.GetExtension(pathToGame);

            Console.WriteLine($"Extension: {gameExtension}");

            switch (gameExtension)
            {
                case ".zip":
                    await RetroArchHelper.ExtractFromZip(pathToGame, outputTempPath);
                    break;

                case ".7z":
                    await RetroArchHelper.ExtractFrom7z(pathToGame, outputTempPath);
                    break;
            }


            if (filesFromOutputPath.Any(f => RetroArchHelper.UnallowedExtension.Contains(Path.GetExtension(f))))
            {
                Console.WriteLine("che");
                throw new Exception($"Unable to load {game}. Please open RetroArch and load it manually. Reason: Unexpecting game extension");
            }

            //TODO: Create a list with all the exceptions
            var firstFileFromTemp = Directory.GetFiles(outputTempPath)
                .Where(f =>
                    RetroArchHelper.AllowedGameExtensions
                    .Contains(Path.GetExtension(f)))
                .FirstOrDefault();

            Console.WriteLine($"First file {firstFileFromTemp}");

            if (firstFileFromTemp is null)
            {
                throw new Exception($"Cannot load files from {outputTempPath}");
            }

            var command = $"{setting.RetroArchPath}retroarch.exe --verbose -L '{setting.RetroArchCorePath}{core}' '{firstFileFromTemp}'";

            await RetroArchHelper.ExecuteCommandWrapper(command);
        }

        public static async void StartRetroArch(Setting setting)
        {
            try
            {
                var command = $"{setting.RetroArchPath}retroarch.exe --menu --verbose";

                await RetroArchHelper.ExecuteCommandWrapper(command);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
