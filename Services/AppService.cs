using RetroClient.Data;
using RetroClient.Models;
using RetroClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ElectronNET.API;

namespace RetroClient.Services
{
	public class AppService
	{

        public static void Close(Action beforeExitAction=null)
        {
            if (beforeExitAction == null)
            {
                Electron.App.Quit();
                return;
            }
            
            Task.Run(() => 
            {
               beforeExitAction.Invoke(); 
            })
            .ContinueWith(t => 
            {
                Electron.App.Quit();
            });

        } 
	}   
}
