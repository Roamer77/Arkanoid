using System;
using UnityEngine;

public class SlowDownBallBlock : Block
{
    public static Action<float> ReduceBallSpeed;
    
    [SerializeField]
    private float _speedDibuff;

    protected override void OnBlockCollid()
    { 
        ReduceBallSpeed?.Invoke(_speedDibuff);
        base.OnBlockCollid();  
    }
}
