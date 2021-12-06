using RetroClient.Models;
using RetroClient.Helpers;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RetroClient.Services
{
    public class RetroArchService
    {

        public static async Task StartGame(Setting setting, string core, string game)
        {
            Thread retroStartGameThread = new Thread(async() =>
            {

                var outputTempPath = setting.GamesTempPath;
                var filesFromOutputPath = Directory.GetFiles(outputTempPath).ToList();

                filesFromOutputPath.ForEach(f =>
                {
                    File.Delete(f);
                });

                var pathToGame = $"{setting.GamesPath}{game}";
                var gameExtension = Path.GetExtension(pathToGame);

                switch (gameExtension)
                {
                    case ".zip":
                        await RetroArchHelper.ExtractFromZip(pathToGame, outputTempPath);
                        break;

                    case ".7z":
                        await RetroArchHelper.ExtractFrom7z(pathToGame, outputTempPath);
                        break;
                }

                //TODO: Create a list with all the exceptions
                var firstFileFromTemp = Directory
                    .GetFiles(outputTempPath)
                    .FirstOrDefault(f =>
                        RetroArchHelper.AllowedGameExtensions.Contains(Path.GetExtension(f))
                    );

                if (firstFileFromTemp is null)
                {
                    throw new Exception($"Unable to load {game}. Reason: Unexpecting game extension");
                }

                var command = $"{setting.RetroArchPath}retroarch.exe --verbose -L '{setting.RetroArchCorePath}{core}' '{firstFileFromTemp}'";

                Console.WriteLine(command);

                //We can run the retroarch execution in another thread so we dont freeze the Blazor UI thread
                await RetroArchHelper.ExecuteCommandWrapper(command);
            });

            retroStartGameThread.Start();
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
