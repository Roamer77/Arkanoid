using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager 
{
    public Action<GameState> OnGameStateChange;

    private static GameStateManager _instance;

    public static GameStateManager Instance
    {
        get
        {
            if(_instance ==null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }

    private GameStateManager(){}

    public GameState CurrentGameState { get; private set; }

    public void SetState(GameState newGameState)
    {
        if(newGameState == CurrentGameState)
        {
            return;
        }
        CurrentGameState = newGameState;
        OnGameStateChange?.Invoke(CurrentGameState);
    }
}
