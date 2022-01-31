using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action<SoundType> PlayGetBonusSound;

    [SerializeField] private Vector2 _startVelocity;

    [SerializeField] private GameObject _startBallPosition;

    [SerializeField] private GameObject _startPlayerPosition;

    private Renderer _renderer;

    [SerializeField]
    private BallSpawner _ballSpawner;

    private Ball _ball;

    private float _platformWidth;
    private Camera _camera;
    private Vector2 _cameraHalfSize;
  
    private bool isGameEnd = false;

    private void Awake()
    {
        _ball = _ballSpawner.Pool.Get();
        _ball.transform.SetParent(transform);
        _ball.transform.position = _startBallPosition.transform.position;
        _ball.BallRigidBody.simulated = false;
        _platformWidth = GetComponent<BoxCollider2D>().size.x;

        _camera = Camera.main;
        _cameraHalfSize = new Vector2
        {
            x = _camera.orthographicSize * _camera.aspect,
            y = _camera.orthographicSize
        };

        _renderer = GetComponent<Renderer>();
        _ball.TrailRenderer.enabled = false;

        IncreasePlatformWidth.IncreaseWidth += IncreaseWidth;
        BottomPlatform.RestartLevel += SetPlayerToStartPostion;
        BlockSpawner.AllBlocksDestroyed += ReturnBallToStartPosition;  
        GameManager.SpeedBuffedPerLevel += IncreaseBallSpeed;
        GameManager.GameWasEnd += onGameEnd;
        GameStateManager.Instance.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState newGameState)
    {
        enabled = newGameState == GameState.Gamepaly;
    }

    void OnDestroy() 
    {
        BlockSpawner.AllBlocksDestroyed -= ReturnBallToStartPosition;
        IncreasePlatformWidth.IncreaseWidth -= IncreaseWidth;
        BottomPlatform.RestartLevel -= SetPlayerToStartPostion;
        GameManager.SpeedBuffedPerLevel -= IncreaseBallSpeed;
        GameManager.GameWasEnd -= onGameEnd; 
        GameStateManager.Instance.OnGameStateChange -= OnGameStateChange;
    }
    void FixedUpdate()
    {
        if(isGameEnd != true)
        {
            var mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var border = _cameraHalfSize.x - _platformWidth / 2;
            var newPosition = new Vector3
            {
                x = Mathf.Clamp(mouseInput.x, -border, border),
                y = transform.position.y,
                z = transform.position.z
            };

            if (!_ball.IsInGame)
            {
                var transform1 = _ball.transform;
                transform1.position = new Vector3(gameObject.transform.position.x, transform1.position.y);
            }

            transform.position = newPosition;

            if (!_ball.IsInGame)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    _ball.transform.SetParent(null);
                    _ball.BallRigidBody.simulated = true;
                    _ball.BallRigidBody.velocity = _startVelocity;
                    _ball.IsInGame = true;
                    _ball.TrailRenderer.enabled = true;
                }
            }
        }
    }

       
    private void IncreaseBallSpeed(float bonus)
    {
        _startVelocity *= bonus;
    }

    private void IncreaseWidth(float bonusWidht, float seconds)
    {
        PlayGetBonusSound?.Invoke(SoundType.GetBonus);
        StartCoroutine(CoroutineSample(bonusWidht,seconds));
    }
    
    private IEnumerator CoroutineSample(float bonusWidht, float seconds)
    {
        transform.localScale += new Vector3(bonusWidht,0,0);
        yield return new WaitForSeconds(seconds); 
        transform.localScale -= new Vector3(bonusWidht,0,0);  
    }
    private void ReturnBallToStartPosition()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Ball"))
        {
          _ballSpawner.Pool.Release(item.gameObject.GetComponent<Ball>());  
        } 
        _ball = _ballSpawner.Pool.Get();
        _ball.transform.SetParent(transform);
        _ball.transform.position = _startBallPosition.transform.position;
        _ball.BallRigidBody.simulated = false;
        _ball.IsInGame = false;
        _ball.TrailRenderer.enabled = false;
    }

    private void SetPlayerToStartPostion()
    {
        transform.position = _startPlayerPosition.transform.position;
        _ball = _ballSpawner.Pool.Get();
        ReturnBallToStartPosition();   
    }

    private void onGameEnd()
    {
        isGameEnd = !isGameEnd;
    }
}
