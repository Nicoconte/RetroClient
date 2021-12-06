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

        public bool IsUserPlaying { get; private set; } = false;
        public string ActiveGameName { get; private set; } = string.Empty;

        public event Action OnUserPlayStateChange;

        public void SetUserPlayState(bool newState, string currentGameName)
        {
            IsUserPlaying = newState;
            ActiveGameName = currentGameName;
            NotifyStateChanged();
        } 

        private void NotifyStateChanged() 
        {
            OnUserPlayStateChange?.Invoke();
        }

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
