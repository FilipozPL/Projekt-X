using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int _width, _height;
    [SerializeField] Transform _camera;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] List<MaterialScriptableObject> materials;
    [SerializeField] TilesStorage tilesStorage;
    void Start()
    {
        GenerateGrid();   
    }
    // Generacja planszy
    void GenerateGrid()
    {
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                // Stworzenie nowej płutki na pozycji (x, y)
                var newTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                newTile.name = $"{x} {y}";
                // Ustawienie materiału płytki na wylosowany materiał
                Tile tileScript = newTile.GetComponent<Tile>();
                tileScript.SetMaterial(GetRandomMaterial());
                // Dodanie płytki do słownika płytek
                tilesStorage.tiles.Add(new Vector2(x, y), newTile.GetComponent<Tile>());
            }
        }
        
        // Ustawienie kamery na środku planszy
        _camera.transform.position = new Vector3(_width/2, _height/2, -10);
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
