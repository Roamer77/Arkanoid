using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;


public class TimeRewardDisplayer : MonoBehaviour
{
    private TextMeshPro _text;

    void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        Ball.SpendAttempt += DisplayAttemptIndex;   
        GameManager.GetInfoAboutLevel += DisplayAtteptIndex;
    }

    void OnDestroy() 
    {
        Ball.SpendAttempt -= DisplayAttemptIndex;
        GameManager.GetInfoAboutLevel += DisplayAtteptIndex;    
    }

    private void DisplayAttemptIndex(int value)
    {
        _text.SetText($"Time rewind : {value.ToString()}");
    }
    private void DisplayAtteptIndex(LevelInfo value)
    {
        _text.SetText($"Time rewind : {value.TimeRewindAttapts.ToString()}");
    }
}
