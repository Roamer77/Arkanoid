using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IDiable
{
    public static Action<SoundType> PlayBounceSound;  
    public static Action<SoundType> PlayHitSound; 
    public static Action<SoundType> PlayGetBonusSound; 
    public static Action<SoundType> PlayGetDibuffSound;  

    [SerializeField] private GameObject _rewindPoint;
   
    private GameObject _rewindPointInstance;
    [SerializeField] private GameObject _rewindEffect;

    [SerializeField] private Rigidbody2D _ballRigidBody;

    private SpriteRenderer _spriteRenderer;

    public TrailRenderer TrailRenderer;

    private bool _isRewindig = false;

    public int RewindTime = 2;

    public List<PointInTime> previosPositions = new List<PointInTime>();

    public Rigidbody2D BallRigidBody
    {
        get
        {
            if (_ballRigidBody == null)
            {
                _ballRigidBody = GetComponent<Rigidbody2D>();
            }

            return _ballRigidBody;
        }
    }

    public bool IsInGame { get; set; }

    private void Awake()
    {
        IsInGame = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SlowDownBallBlock.ReduceBallSpeed += SlowDownBall;
        GameStateManager.Instance.OnGameStateChange += OnGameStateChange;
        Player.OnRewind += OnRewind;
    }

    private void OnRewind(bool value)
    {
        _isRewindig = value;
    }

    void OnDestroy() 
    {
        SlowDownBallBlock.ReduceBallSpeed -= SlowDownBall;
        GameStateManager.Instance.OnGameStateChange -= OnGameStateChange;
    }

    private void OnGameStateChange(GameState newGameState)
    {
       _ballRigidBody.simulated = newGameState == GameState.Gamepaly ? true : false;
    }

    void FixedUpdate() 
    {
        if(_isRewindig)
        {
            Rewind();
        }else
        {
            Record();
        }
    }

    private void Rewind()
    {
        if(previosPositions.Count > 0)
        {
            transform.position = previosPositions[0].Positon;
            _ballRigidBody.velocity = previosPositions[0].Velosity;
            previosPositions.RemoveAt(0);
        }
        if(previosPositions.Count == 0 )
        {
            _isRewindig = false;
            _ballRigidBody.isKinematic = false;
            if(_ballRigidBody.velocity == Vector2.zero)
            {
                print("_ballRigidBody.velocity == 0");
                _ballRigidBody.velocity = new Vector2(0,7);
            }
        }
    }
    private void Record()
    {   
        if(previosPositions.Count > Mathf.Round( RewindTime / Time.deltaTime)  )
        {
            previosPositions.RemoveAt(previosPositions.Count - 1);
        }  
         previosPositions.Insert(0,new PointInTime(transform.position, _ballRigidBody.velocity) );
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!col.gameObject.CompareTag("Block"))
        {
            PlayBounceSound?.Invoke(SoundType.Bounce);
        }

        if(col.gameObject.CompareTag("Block"))
        {
            PlayHitSound?.Invoke(SoundType.BlockHit);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            var list = new List<ContactPoint2D>();
            col.GetContacts(list);
           
            var hitPoint = list[0].point;
            var platformCenter = col.collider.bounds.center.x;
            var platformBorder = col.collider.bounds.size.x / 2;
            var offset = platformCenter - hitPoint.x;
           
            var maxBounceAngle = 75;
            var bounceAngle = (offset / platformBorder) * maxBounceAngle;

            var currentAngle = Vector2.SignedAngle(Vector2.up,_ballRigidBody.velocity);
            var newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle,Vector3.forward);
            _ballRigidBody.velocity = rotation * Vector2.up * _ballRigidBody.velocity.magnitude;
        }
        
    }

    private void SlowDownBall(float dibuffSpeedValue)
    {
        PlayGetDibuffSound?.Invoke(SoundType.GetDibuff);
        if(gameObject.activeSelf == true)
        {
            StartCoroutine(SlowDownBallCarutine(dibuffSpeedValue));
        }
    }

    private IEnumerator SlowDownBallCarutine(float dibuffSpeedValue)
    {
        _ballRigidBody.velocity =  _ballRigidBody.velocity / dibuffSpeedValue;
        _spriteRenderer.color = Color.blue;
        yield return new WaitForSecondsRealtime(3);
        _ballRigidBody.velocity =  _ballRigidBody.velocity * dibuffSpeedValue;
        _spriteRenderer.color = new Color32(255,25,144,255);
    }

    public void OnDie()
    {
        Destroy(gameObject);
    }
    
    public void SpawnRewindPoint()
    {
        _rewindPointInstance = Instantiate(_rewindPoint,previosPositions[previosPositions.Count - 1].Positon,Quaternion.identity);
    }

    public void DestroyRewindPoint()
    {
        if(_isRewindig == false)
        {
            if(_rewindPointInstance != null)
            {
                Destroy(_rewindPointInstance);
            }
        }
    }
}
