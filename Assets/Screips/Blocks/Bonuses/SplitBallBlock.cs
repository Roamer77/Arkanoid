using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBallBlock : Block
{
    public static Action<int,Vector3, Vector2> SplitBall;
    void OnCollisionEnter2D(Collision2D col) 
    {
        var ball = col.gameObject.GetComponent<Rigidbody2D>();
        var amountOfballs = 2; 
        
        if(col.gameObject.CompareTag("Ball"))
        {
            SplitBall?.Invoke(amountOfballs, col.transform.position, ball.velocity);
        }
        
        base.OnBlockCollid();
    }
}
