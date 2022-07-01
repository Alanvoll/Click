using UnityEngine;

public interface IAudioManager : IService
{
    public bool IsMute { get; }
    public void SetMuteSound(bool isMute);

    public void PlaySFX(AudioClipType clipType);
}