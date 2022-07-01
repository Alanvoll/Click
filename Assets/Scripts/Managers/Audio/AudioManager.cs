using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour, IAudioManager
{
    [SerializeField] private GameObject _audioSourcePrefab;
    [SerializeField] private AudioClipSettings _audioSettings;
    [SerializeField] private List<AudioSource> _audioSources;
    private bool _isMute;


    public bool IsMute => _isMute;

    private void Awake()
    {
        ServiceProvider.AddService<IAudioManager>(this);
        LoadSettings();
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SaveSettings();
    }

    public void PlaySFX(AudioClipType clipType)
    {
        var clip = _audioSettings.GetClipByType(clipType);
        if(!clip)
            return;

        var audioSource = GetAudioSource();
        audioSource.PlayOneShot(clip);
    }

    public void SetMuteSound(bool isMute)
    {
        _isMute = isMute;
        foreach (var audioSource in _audioSources)
        {
            audioSource.volume = _isMute ? 0 : 1;
        }
    }

    private AudioSource GetAudioSource()
    {
        foreach (var source in _audioSources)
        {
            if (source.isPlaying)
                continue;

            return source;
        }

        var audioSource = Instantiate(_audioSourcePrefab, transform).GetComponent<AudioSource>();
        audioSource.volume = _isMute ? 0 : 1;
        _audioSources.Add(audioSource);
        
        return audioSource;
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("IsMute"))
            _isMute = PlayerPrefs.GetInt("IsMute") == 1;
        else
            _isMute = false;
        
        foreach (var audioSource in _audioSources)
        {
            audioSource.volume = _isMute ? 0 : 1;
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("IsMute", _isMute ? 1 : 0);
        PlayerPrefs.Save();
    }
}