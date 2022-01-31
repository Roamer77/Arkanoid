using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public ObjectPool<Ball> Pool { get; private set;}

    [SerializeField]
    private Ball _ballPrefub;
    void Awake() 
    {
        Pool = new ObjectPool<Ball>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool , OnDestroyPollObject, false, 10);
        SplitBallBlock.SplitBall += SpawnBallAfterSplit;

        for (int i = 0; i < 3; i++)
        {
            CreatePooledItem().gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {
        SplitBallBlock.SplitBall -= SpawnBallAfterSplit;
    }
    public void SpawnBallAfterSplit(int amount, Vector3 ballInGamePosition, Vector2 ballInGameVelocity)
    {
        for (var i = 0; i < amount; i++)
        {
            var ball = Pool.Get();
            ball.transform.position = ballInGamePosition;
            Quaternion angel = Quaternion.AngleAxis(15 * i,Vector3.forward);
            ball.BallRigidBody.velocity = angel * Vector2.up * ballInGameVelocity.magnitude; 
        }
    
    }
    private Ball CreatePooledItem ()
    {
       return Instantiate(_ballPrefub, new Vector3(0,0,0), Quaternion.identity);
    }
    private void OnReturnedToPool(Ball item)
    {
        item.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Ball item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnDestroyPollObject(Ball item)
    {
        Destroy(item.gameObject);
    }
}
