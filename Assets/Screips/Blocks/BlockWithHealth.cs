using System;
using TMPro;
using UnityEngine;

public class BlockWithHealth : Block
{
    public int Health { get; set; }

    [SerializeField] private TextMeshPro _textMeshPro;

    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        Health = UnityEngine.Random.Range(1,3);
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        _textMeshPro.SetText(Health.ToString());
    }

    protected override void OnBlockCollid()
    { 
        _particleSystem.Play();
        Health--;
        _textMeshPro.SetText(Health.ToString());
        if (Health == 0)
        {     
            _particleSystem.transform.SetParent(null);
            base.OnBlockCollid();
        }
    }
}
