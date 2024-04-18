using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetUp : MonoBehaviour
{
    [SerializeField] MaterialScriptableObject material;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        spriteRenderer.sprite = material.sprite;
    }
}
