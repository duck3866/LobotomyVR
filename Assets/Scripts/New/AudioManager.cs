using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float MAX_VOLUME = 100f; // 최대 볼륨
    [SerializeField] private AudioSource bgmSource; // 배경음악 소스
    public float BGMVolume { get; private set; } = 100f;
    public float SfxVolume { get; private set; } = 100f;

    private float _bgmVolumeRatio = 1f; // 볼륨 비율

    private float _sfxVolumeRatio = 1f;

    public void SetBGMPitch(float pitch)
    {
        bgmSource.pitch = pitch;
    }
    public void SetBGMVolumeRatio(float volume)
    {
        if (volume < 0 || volume > 100f)
        {
            Debug.LogError("볼륨이 올바르지 않음");
            return;
        }

        BGMVolume = volume;
        _bgmVolumeRatio = volume/MAX_VOLUME;

        bgmSource.volume = _bgmVolumeRatio;
    }

    public void SetSfxVolumeRatio(float volume)
    {
        if (volume < 0 || volume > 100f)
        {
            Debug.LogError("볼륨이 올바르지 않음");
            return;
        }

        SfxVolume = volume;
        _sfxVolumeRatio = volume/MAX_VOLUME;
    }

    public void PlaySoundEffect(AudioClip clip, Vector3 position, float volume)
    {
        if (clip == null)
        {
            Debug.LogError("오디오 클립이 올바르지 않음");
            return;
        }

        if (volume < 0 || volume > 1f)
        {
            Debug.LogError("볼륨이 올바르지 않음");
            return;
        }
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void SetBGMSource(AudioSource audioSource)
    {
        bgmSource = audioSource;
    }
    
    public void PlayBackgroundMusic(AudioClip clip, float volume = 1f)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volume * _bgmVolumeRatio;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBackgroundMusic()
    {
        bgmSource.Stop();
    }

    public void SetBGMState(bool isPause)
    {
        if (isPause)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }
}
