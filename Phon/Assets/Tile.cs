using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MaterialScriptableObject material;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetMaterial(MaterialScriptableObject newMaterial)
    {
        if(newMaterial != null)
        {
            material = newMaterial;
            spriteRenderer.sprite = material.sprite;
        }
    }

    public MaterialScriptableObject GetMaterial()
    {
        return material;
    }
}
