using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "ScriptableObjects/Material", order = 1)]
public class MaterialScriptableObject : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    [Range(0.01f, 1)] public float spawnChance;
}
