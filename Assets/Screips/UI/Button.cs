using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private RectTransform _button;

    void Awake()
    {
        _button = GetComponent<RectTransform>();
    }

    public void OnButtonHoverStart()
    {
       _button.localScale = new Vector3(1.1f,1.1f,0f);
    }

    public void OnButtonHoverEnd()
    {
       _button.localScale = new Vector3(1f,1f,0f);
    }
}
