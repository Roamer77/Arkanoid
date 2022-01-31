using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    [SerializeField]
    private List<Sound> _sounds = new List<Sound>();

    [SerializeField]
    private AudioSource _audioSource; 

    void Awake()
    {
       BottomPlatform.PlayLoseConditionSound += PlaySound;
       Ball.PlayBounceSound += PlaySound;
       Ball.PlayHitSound += PlaySound;
       Ball.PlayGetDibuffSound +=PlaySound;
       GameManager.PlayWinConditionSound += PlaySound;
       Player.PlayGetBonusSound += PlaySound;
       
    }

    void OnDestroy() 
    {
        BottomPlatform.PlayLoseConditionSound -= PlaySound;
        Ball.PlayBounceSound -= PlaySound;
        Ball.PlayHitSound -= PlaySound;
        Ball.PlayGetDibuffSound -=PlaySound;
        GameManager.PlayWinConditionSound -= PlaySound;
        Player.PlayGetBonusSound -= PlaySound;
    }

    private AudioClip FindSound(SoundType type)
    {
      var sound = _sounds.Find(item => item.Type.Equals(type));
      return sound.AudioClip;
    }

    private void PlaySound(SoundType type)
    {
        _audioSource.PlayOneShot(FindSound(type));
    }

}
