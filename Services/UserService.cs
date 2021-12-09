
using System;

public class UserService 
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
}