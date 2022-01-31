using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static Action<int, List<BlockInfo>> SpawnBlocks;
  
  public static Action<LevelInfo> GetInfoAboutLevel;

  public static Action<float> SpeedBuffedPerLevel;

  
  public static Action DestroyAllBlocksInLevel;
  public static Action GameWasEnd;

  public static Action<SoundType> PlayWinConditionSound;

  public static Action<bool> OnChangeAmountOfBallsInGame;

  public BallSpawner BallSpawner;

  private int _userScore {get; set;}

  private LevelInfo[] _allLevels {get; set;}

  private int _currentLevel = 0;

  private bool isGameEnd = false;

  private List<GameObject> _instanceOfBall = new List<GameObject>();

  void Awake()
  {
    BlockSpawner.AllBlocksDestroyed += StartLevel;
    BlockSpawner.BlockDestroyed += SetUserScore;
    BottomPlatform.RestartLevel += RestartCurrentLevel;
    PauseMenu.SaveUserProgress += SaveCurrentPlayerProgress;
  }
  void Start() 
  {
      InitLevels();
      if(MainMenu.PlayedChooseLoadGame)
      {
        StartGameFromLoadSave();
      }
      else
      {
        StartLevel();
      }
  }

  void Update() 
  {
    if(isGameEnd && Input.GetKey(KeyCode.Space))
      {
         SceneManager.LoadScene(0); 
      }
    if(BallSpawner.Pool.CountActive == 1)
    {
      OnChangeAmountOfBallsInGame?.Invoke(false);
    }else
    {
      OnChangeAmountOfBallsInGame?.Invoke(true);
    }
  }

  void OnDestroy()
  {
    BlockSpawner.AllBlocksDestroyed -= StartLevel;
    BlockSpawner.BlockDestroyed -= SetUserScore;
    BottomPlatform.RestartLevel -= RestartCurrentLevel;
    PauseMenu.SaveUserProgress -= SaveCurrentPlayerProgress;
  }

  private void StartLevel()
  {
    PrepareLevel(_currentLevel);
    _currentLevel++;
  }

  private void PrepareLevel(int levelNumber)
  {
    if(levelNumber < _allLevels.Length)
    {
      var level = _allLevels[levelNumber];
      SpawnBlocks?.Invoke(level.AmointOfRows, level.Blocks());
      GetInfoAboutLevel?.Invoke(level);
      SpeedBuffedPerLevel?.Invoke(level.BuffSpeedBallPefLevel);
    }
    else
    {
      GameEnd();
    }
  }

  private void GameEnd()
  {
      GameWasEnd?.Invoke();   
      isGameEnd = true;
      PlayWinConditionSound?.Invoke(SoundType.WinCondition);
  }
  private void InitLevels()
  {
    _allLevels = Resources.LoadAll<LevelInfo>("Levels");
  }

  private void RestartCurrentLevel()
  {
      DestroyAllBlocksInLevel?.Invoke();
      PrepareLevel(_currentLevel - 1); 
  }

  private void SetUserScore(int score) => _userScore = score;

  private void SaveCurrentPlayerProgress()
  {
    var time = DateTime.Now.ToString("yyyy'-'MM'-'dd'_t_:'HH':'mm':'ss");
    var currentProgress = new PlayerData(_currentLevel - 1, _userScore, "Save1", time);
    SaveDataSystem.SaveData(currentProgress);
  }
  
  private void StartGameFromLoadSave()
  {
     var playerSave = SaveDataSystem.LoadData();
     _currentLevel = playerSave.LevelIndex;
     StartLevel();
  }
  
}



  