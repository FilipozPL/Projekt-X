using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int _width, _height;
    [SerializeField] Transform _camera;
    [SerializeField] List<GameObject> _tilePrefabs;
    Dictionary<Vector2, GameObject> _tiles;
    void Start()
    {
        GenerateGrid();   
    }
    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, GameObject>();
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                float spawn = Random.Range(0.01f, 1f);
                GameObject newTile;
                switch(spawn)
                {
                    case  < 0.3f:
                        newTile = Instantiate(_tilePrefabs.Find((x) => x.name == "Iron Tile"), new Vector3(x, y), Quaternion.identity);
                        break;
                    case < 0.6f:
                        newTile = Instantiate(_tilePrefabs.Find((x) => x.name == "Gold Tile"), new Vector3(x, y), Quaternion.identity);
                        break;
                    default:
                        newTile = Instantiate(_tilePrefabs.Find((x) => x.name == "Copper Tile"), new Vector3(x, y), Quaternion.identity);
                        break;

                }
                newTile.name = $"{x} {y}";
                _tiles.Add(new Vector2(x, y), newTile);
            }
        }
        
        _camera.transform.position = new Vector3(_width/2, _height/2, -10);
    }
    public GameObject GetTile(Vector2 position)
    {
        if(_tiles.TryGetValue(position, out var tile))
        {
            return tile;
        }
        return null;
    }
}
