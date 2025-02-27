using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    public float SoundEffectVolume => soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;
    public float MusicVolume => musicVolume;

    private AudioSource musicAudioSource;
    public AudioSource MusicAudioSource => musicAudioSource;
    public AudioClip musicClip;

    private string MusicVolumKey = "MusicVolumeKey";
    private string SoundEffectVolumeKey = "SoundEffectVolume";

    public SoundSource soundSourcePrefabs;

    private void Awake()
    {
        musicVolume = PlayerPrefs.GetFloat(MusicVolumKey, 0);
        soundEffectVolume = PlayerPrefs.GetFloat(SoundEffectVolumeKey, 0);

        instance = this;
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }

    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("����� AudioClip�� null�Դϴ�! AudioManager���� Ȯ���ϼ���.");
            return;
        }
        SoundSource obj = Instantiate(instance.soundSourcePrefabs);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, instance.soundEffectVolume, instance.soundEffectPitchVariance);
    }

    public void SetSoundEffectVolume(float _soundEffectVolule)
    {
        soundEffectVolume = _soundEffectVolule;
        PlayerPrefs.SetFloat(SoundEffectVolumeKey, _soundEffectVolule);
    }

    public void SetMusicVolumeSave(float value)
    {
        PlayerPrefs.SetFloat(MusicVolumKey, value);
    }


}
