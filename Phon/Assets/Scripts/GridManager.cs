using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int _width, _height;
    [SerializeField] Transform _camera;
    [SerializeField] List<GameObject> _tilePrefabs;
    void Start()
    {
        GenerateGrid();   
    }
    void GenerateGrid()
    {
        for(int x = 0; x < _width; x++)
        {
            for(int y = 0; y < _height; y++)
            {
                float spawn = Random.Range(0.01f, 1f);
                GameObject newTile;
                switch(spawn)
                {
                    case  < 0.5f:
                        newTile = Instantiate(_tilePrefabs.Find((x) => x.name == "Iron Tile"), new Vector3(x, y), Quaternion.identity);
                        break;
                    default:
                        newTile = Instantiate(_tilePrefabs.Find((x) => x.name == "Copper Tile"), new Vector3(x, y), Quaternion.identity);
                        break;

                }
                newTile.name = $"{x} {y}";
            }
        }
        
        _camera.transform.position = new Vector3(_width/2, _height/2, -10);
    }
}
