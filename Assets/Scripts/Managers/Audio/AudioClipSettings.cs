using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioClip Settings", menuName = "GameSettings/AudioClipSettings", order = 0)]
public class AudioClipSettings : ScriptableObject
{
    [SerializeField] private List<AudioClipSetting> _settings;

    public AudioClip GetClipByType(AudioClipType type)
    {
        return _settings.First(setting => setting.Type == type).Clip;
    }

    [Serializable]
    private class AudioClipSetting
    {
        [SerializeField] private AudioClipType _type;
        [SerializeField] private AudioClip _clip;

        public AudioClipType Type => _type;

        public AudioClip Clip => _clip;
    }
}