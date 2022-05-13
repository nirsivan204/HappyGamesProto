using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMGR : MonoBehaviour
{
    [Serializable]
    public class SoundType_And_Ref
    {
        public SoundTypes SoundType;
        public AudioClip AudioClipRef;
    }

    [Serializable]
    public class SoundSource_And_Ref
    {
        public AudioSourceTypes SourceType;
        public AudioSource AudioSourceRef;
    }


    [SerializeField]
    private List<SoundType_And_Ref> SoundType_And_Ref_List = new List<SoundType_And_Ref>();

    [SerializeField]
    private List<SoundSource_And_Ref> SoundSource_And_Ref_List = new List<SoundSource_And_Ref>();


    public enum SoundTypes
    {
        None = 0,

        mixerRun = 1,
        mixerEnd = 2,

        // UI_Sounds
        Click_01 = 100,

        // Gameplay Sounds
        SUCCESS = 201,
        FAIL = 202,
        // Music
        BG_Music_1 = 300,
        BG_Music_2 = 301,
        //other
    }

    public enum AudioSourceTypes
    {
        None,
        Mixer,
        UI,
        Gameplay,
        Music,
    }

    internal void Init()
    {

    }

    public void Play_Sound(SoundTypes soundType, bool isLoop = false)
    {
        AudioClip clip = Get_AudioClip_Of(soundType);

        AudioSource source = Get_AudioSource_Of(soundType);

        source.loop = isLoop;

        Play_Sound(clip, source);

    }

    public void Stop_Sound(SoundTypes soundType)
    {
        AudioSource source = Get_AudioSource_Of(soundType);

        Stop_Sound(source);

    }

    private void Play_Sound(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.pitch = 1;

        source.Play();
    }

    private void Stop_Sound(AudioSource source)
    {
        source.Stop();
    }


    private AudioSource Get_AudioSource_Of(SoundTypes soundType)
    {
        switch (soundType)
        {
            case SoundTypes.None:
                break;
            case SoundTypes.mixerRun:
            case SoundTypes.mixerEnd:
                return Get_AudioSource_By_Type(AudioSourceTypes.Mixer);
            case SoundTypes.Click_01:
                return Get_AudioSource_By_Type(AudioSourceTypes.UI);
            case SoundTypes.BG_Music_1:
            case SoundTypes.BG_Music_2:
                return Get_AudioSource_By_Type(AudioSourceTypes.Music);
            default:
                return Get_AudioSource_By_Type(AudioSourceTypes.Gameplay);
        }

        return null;
    }

    private AudioSource Get_AudioSource_By_Type(AudioSourceTypes audioSourceType)
    {
        for (int i = 0; i < SoundSource_And_Ref_List.Count; i++)
        {
            if (SoundSource_And_Ref_List[i].SourceType == audioSourceType)
            {
                return SoundSource_And_Ref_List[i].AudioSourceRef;
            }
        }

        return null;
    }

    private AudioClip Get_AudioClip_Of(SoundTypes soundType)
    {
        for (int i = 0; i < SoundType_And_Ref_List.Count; i++)
        {
            if (SoundType_And_Ref_List[i].SoundType == soundType)
                return SoundType_And_Ref_List[i].AudioClipRef;
        }

        return null;
    }

    public void AddPitch(SoundTypes soundType, float addedPitch)
    {
        AudioSource source = Get_AudioSource_Of(soundType);
        source.pitch += addedPitch;
    }
}
