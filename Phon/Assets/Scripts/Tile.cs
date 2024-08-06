using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MaterialScriptableObject _material;
    public MaterialScriptableObject Material 
    { 
        get => _material;
        set
        {
            if (value == null)
            {
                _material = value;
                _spriteRenderer.sprite = null;
                return;
            }
            _material = value; 
            _spriteRenderer.sprite = _material.sprite;
        } 
    }

    public Vector2Int Position { get; set; }

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
