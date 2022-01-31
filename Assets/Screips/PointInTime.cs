using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime 
{
    public Vector2 Positon {get; private set;}
    public Vector2 Velosity {get; private set;}

    public PointInTime(Vector2 position, Vector2 velosity)
    {
        Positon = position;
        Velosity = velosity;
    }
}
