using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomPlatform : MonoBehaviour
{
    public static Action RestartLevel;

    public static Action<SoundType> PlayLoseConditionSound;

    [SerializeField]
    private BallSpawner ballSpawner;
    
    void Awake() 
    {
       // SplitBallBlock.SplitBall += OnBallSplit;    
    }
    void OnDestroy() 
    {
        //SplitBallBlock.SplitBall -= OnBallSplit;
    }

    private void OnBallSplit()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Ball"))
        {
          ballSpawner.Pool.Release(col.gameObject.GetComponent<Ball>());
             if(ballSpawner.Pool.CountActive == 0 )
             {
                PlayLoseConditionSound?.Invoke(SoundType.LoseCondition);
                RestartLevel?.Invoke();
             }
        }
    }
}
