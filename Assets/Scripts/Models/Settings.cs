using UnityEngine;

namespace Models
{
    public class Settings
    {
        private readonly AudioSource _musicAudioSource;
        private readonly AudioSource _soundAudioSource;
        
        private bool _isMusicMuted;
        private bool _isSoundMuted;

        private const string _infoUrl = "https://t.me/oduvaanchikk";
        
        public Settings(AudioSource musicAudioSource, AudioSource soundAudioSource)
        {
            _musicAudioSource = musicAudioSource;
            _soundAudioSource = soundAudioSource;
        }
        
        public void SwitchMusic()
        {
            _isMusicMuted = !_isMusicMuted;
            _musicAudioSource.mute = _isMusicMuted;
        }
        
        public void SwitchSound()
        {
            _isSoundMuted = !_isSoundMuted;
            _soundAudioSource.mute = _isSoundMuted;
        }

        public void GetInfo()
        {
            Application.OpenURL(_infoUrl);
        }
    }
}