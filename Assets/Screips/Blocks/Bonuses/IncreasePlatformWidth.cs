using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlatformWidth : Block
{
    public static Action<float, float> IncreaseWidth;

    [SerializeField] private float _bonusWidth = 0.5f;
    [SerializeField] private float _BuffTime = 7;

     protected override void OnBlockCollid()
    { 
        IncreaseWidth?.Invoke(_bonusWidth,_BuffTime);
        base.OnBlockCollid();  
    }
}
