using System;
using UnityEngine;

namespace Models
{
    public class Settings
    {
        public Action OnMusicStateChanged;
        public Action OnSoundStateChanged;
        
        private readonly AudioManager _audioManager;

        private bool _isMusicMuted;
        private bool _isBallSoundMuted;

        private const string _infoUrl = "https://www.eulatemplate.com/live.php?token=iBicDRcFQYwWv6j16iMEzOKEtxwDys87";

        public Settings(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
        
        public void SwitchMusic()
        {
            _isMusicMuted = !_isMusicMuted;
            _audioManager.IsMusicSourceMuted = _isMusicMuted;
            
            OnMusicStateChanged?.Invoke();
        }
        
        public void SwitchSound()
        {
            _isBallSoundMuted = !_isBallSoundMuted;
            _audioManager.IsEffectsSourceMuted = _isBallSoundMuted;
            
            OnSoundStateChanged?.Invoke();
        }

        public void GetInfo()
        {
            Application.OpenURL(_infoUrl);
        }
    }
}