using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ESoundType
{
    ButtonClick,
    Bg_Lobby,
    Bg_Game,
    CompleOne,
    Win, 
    Los
}
[System.Serializable]
public class SOSound
{
    public ESoundType SoundType;
    public AudioClip AudioClip;
}
[System.Serializable]
public class SoundItem
{
    public List<SOSound> Items;
    public AudioClip Get(ESoundType type)
    {
        var item = Items.FirstOrDefault(x => x.SoundType == type);
        if (item == null) return Items[0].AudioClip;
        return item.AudioClip;
    }
}
public class SoundManager : SingletonDontDestroy<SoundManager>
{
    [SerializeField] private SoundItem audioClips;
    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource sourceEffect;


    private void Start()
    {
        //SetActiveMusic(PlayPrefSystem.GetBool(KeyStorange.ActiveMusic, true));
        //SetActiveEffect(PlayPrefSystem.GetBool(KeyStorange.ActiveSoundEffect, true));
    }
    public void PlayMusic(ESoundType type)
    {
        PlaySource(type, sourceMusic);
    }
    public void SetActiveMusic(bool isActive)
    {
      //  SetActiveSource(sourceMusic, isActive);
    }
    public void PlayEffect(ESoundType type)
    {
        PlaySource(type, sourceEffect);
    }
    public void SetActiveEffect(bool isActive)
    {
        SetActiveSource(sourceEffect, isActive);
    }



    private void PlaySource(ESoundType type, AudioSource audioSource)
    {
        audioSource.clip = audioClips.Get(type);
        audioSource.Play();
    }
    private void SetActiveSource(AudioSource audioSource, bool isActive)
    {
        audioSource.mute = !isActive;
    }
}
