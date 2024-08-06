using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }
    [SerializeField] internal int width;
    [SerializeField] private int height;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<MaterialScriptableObject> materials;
    [SerializeField] private TilesStorage tilesStorage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateGrid();   
    }
    // Generacja planszy
    private void GenerateGrid()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                // Stworzenie nowej płytki na pozycji (x, y)
                var newTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity, transform);
                newTile.name = $"{x} {y}";
                // Ustawienie materiału płytki na wylosowany materiał
                Tile tileScript = newTile.GetComponent<Tile>();
                tileScript.Material = GetRandomMaterial();
                tileScript.Position = new Vector2Int(x, y);
                // Dodanie płytki do słownika płytek
                tilesStorage.AddTile(new Vector2(x, y), newTile.GetComponent<Tile>());
            }
        }
        
        // Ustawienie kamery na środku planszy
        cameraTransform.position = new Vector3(width/2, height/2, -10);
    }

    // Generacja losowego materiału na podstawie podanej szansy
    MaterialScriptableObject GetRandomMaterial()
    {
        MaterialScriptableObject material = null;
        float random = UnityEngine.Random.Range(0.01f, 1f);
        switch(random)
        {
            case <= 0.3f:
                material = materials.Find(x => x.name == "Iron");
                break;
            case <= 0.6f:
                material = materials.Find(x => x.name == "Gold");
                break;
            default:
                material = materials.Find(x => x.name == "Copper");
                break;
        }
        return material;
    }
}
