using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSource = default;
    [SerializeField] private AudioSource _ballSource = default;
    [SerializeField] private AudioSource _musicSource = default;

    [SerializeField] private AudioClip _uiTap = default;
    [SerializeField] private AudioClip _ballHit = default;
    [SerializeField] private AudioClip _ballWoodHit = default;

    public bool IsMusicSourceMuted
    {
        get => _musicSource.mute;
        set => _musicSource.mute = value;
    }

    public bool IsBallSourceMuted
    {
        get => _ballSource.mute;
        set => _ballSource.mute = value;
    }

    public bool IsUiSourceMuted
    {
        get => _uiSource.mute;
        set => _uiSource.mute = value;
    }
    public void PlayUiSound() { _uiSource.PlayOneShot(_uiTap); }

    public void PlayBallHitSound()
    {
        GetPhysicalSound(_ballSource).PlayOneShot(_ballHit);
    }
    public void PlayBallWoodHitSound()
    {
        GetPhysicalSound(_ballSource).PlayOneShot(_ballWoodHit);
    }

    public void PlayMainTheme() { _musicSource.Play(); }
    public void PauseMainTheme() { _musicSource.Pause(); }
    public void ResumeMainTheme() { _musicSource.UnPause(); }
    public void StopMainTheme() { _musicSource.Stop(); }
    
    private AudioSource GetPhysicalSound(AudioSource source)
    {
        source.pitch = Random.Range(0.5f, 1f);
        return source;
    }
}