using System;
using TMPro;
using UnityEngine;

public class GameAnnouncer : MonoBehaviour
{
    private TextMeshPro _text;

    void Awake()
    {
        _text = GetComponent<TextMeshPro>();
        GameManager.GetInfoAboutLevel += ShowLevelInfo;
        BlockSpawner.BlockDestroyed += ShowDistroedBlocksCounter;
    }
    void OnDisable() 
    {
        GameManager.GetInfoAboutLevel -= ShowLevelInfo;
        BlockSpawner.BlockDestroyed -= ShowDistroedBlocksCounter;
    }

    private void ShowLevelInfo(LevelInfo level)
    {
        _text.SetText(level.LevelName);
    }

    private void ShowDistroedBlocksCounter(int value)
    {
        _text.SetText(value.ToString());
    }
}
