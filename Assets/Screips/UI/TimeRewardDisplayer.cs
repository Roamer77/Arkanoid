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
        Player.SpendAttempt += DisplayAttemptIndex;   
        GameManager.GetInfoAboutLevel += DisplayAtteptIndex;
        GameManager.OnChangeAmountOfBallsInGame += OnBallSplited;
    }

    void OnDestroy() 
    {
        Player.SpendAttempt -= DisplayAttemptIndex;
        GameManager.GetInfoAboutLevel += DisplayAtteptIndex;  
        GameManager.OnChangeAmountOfBallsInGame -= OnBallSplited;  
    }

    private void DisplayAttemptIndex(int value)
    {
        _text.SetText($"Time rewind : {value.ToString()}");
    }
    private void DisplayAtteptIndex(LevelInfo value)
    {
        _text.SetText($"Time rewind : {value.TimeRewindAttapts.ToString()}");
    }

    private void OnBallSplited(bool value)
    {
        _text.color = value ? new Color32(255,0,0,150) : new Color32(255,255,255,255);
    }
}
