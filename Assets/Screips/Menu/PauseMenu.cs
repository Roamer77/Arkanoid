using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static Action SaveUserProgress;

    public GameObject PouseMenuUI;

    [SerializeField] private GameObject _endGameCongratulations;
    
    [SerializeField] private GameObject _endGameMessage;

    private  GameState _currentState;

    void Awake() 
    {
        GameManager.GameWasEnd += OnGameEnd;  
        GameStateManager.Instance.SetState(GameState.Gamepaly); 
    }
    void OnDestroy()
    {
        GameManager.GameWasEnd -= OnGameEnd; 
    }
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _currentState = GameStateManager.Instance.CurrentGameState;
            if(_currentState == GameState.Gamepaly)
            {
                Pause();
            }
            else
            {
                Resume();
            } 
        }
    }

    public void Resume()
    {
        GameStateManager.Instance.SetState(GameState.Gamepaly);
        PouseMenuUI.SetActive(false);
    }
    

    private void Pause()
    {
        GameStateManager.Instance.SetState(GameState.Pouse);
        PouseMenuUI.SetActive(true);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void SaveGame()
    {    
         SaveUserProgress?.Invoke();
    }

    private void OnGameEnd()
    {
        _endGameCongratulations.SetActive(true);
        _endGameMessage.SetActive(true);
    }
}
