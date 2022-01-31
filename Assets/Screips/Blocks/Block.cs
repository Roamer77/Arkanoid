using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event Action<Block> Destroyed;

    private Vector3 _spawnPoint;


    public Vector3 SpawnPoint => _spawnPoint;
    
    public Block()
    {
        
    }
    public Block(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;     
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Ball"))
        {
           OnBlockCollid();
        }
    }

    protected virtual void OnBlockCollid()
    {
        
        Destroy(gameObject);
        Destroyed?.Invoke(this);
        Destroyed = null;
    }
    
}
