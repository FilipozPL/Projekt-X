using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MaterialScriptableObject material;
    public MaterialScriptableObject Material 
    { 
        get { return material; } 
        set 
        { 
            if(value != null)
            {
                material = value; 
                spriteRenderer.sprite = material.sprite;
            }
        } 
    }
    Vector2Int position;
    public Vector2Int Position
    {
        get { return position; }
        set
        {
            if (value != null)
            {
                position = value;
            }
        }
    }
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
