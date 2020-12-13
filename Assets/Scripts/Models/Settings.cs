using System;
using UnityEngine;

namespace Models
{
    public class Settings
    {
        public Action OnMusicStateChanged;
        public Action OnSoundStateChanged;
        
        private bool _isMusicMuted;
        private bool _isBallSoundMuted;

        private const string _infoUrl = "https://t.me/oduvaanchikk";

        public void SwitchMusic()
        {
            _isMusicMuted = !_isMusicMuted;
            //AudioManager.IsMusicMuted = _isMusicMuted;
            
            OnMusicStateChanged?.Invoke();
        }
        
        public void SwitchSound()
        {
            _isBallSoundMuted = !_isBallSoundMuted;
            //AudioManager.IsSoundMuted = _isBallSoundMuted;
            
            OnSoundStateChanged?.Invoke();
        }

        public void GetInfo()
        {
            Application.OpenURL(_infoUrl);
        }
    }
}