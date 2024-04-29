using System.Collections;
using System.Collections.Generic;
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
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
