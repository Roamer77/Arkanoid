using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound 
{
    public AudioClip AudioClip;

    public SoundType Type;

    public Sound (AudioClip audioClip, SoundType type)
    {
        AudioClip = audioClip;
        Type = type;
    }
}
