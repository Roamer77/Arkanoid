using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindEffect : MonoBehaviour
{
   void Awake()
   {
       var camera = Camera.main;
       transform.localScale = new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2  , 0);
   }
}
