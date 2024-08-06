using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Storage", menuName = "Scriptable Objects/Tile Storage", order = 2)]
public class TilesStorage : ScriptableObject
{
    private readonly Dictionary<Vector2, Tile> _tiles = new();

    public void AddTile(Vector2 position, Tile tile)
    {
        _tiles.Add(position, tile);
    }
    
    public Tile GetTile(Vector2 position)
    {
        _tiles.TryGetValue(position, out Tile tile);
        return tile;
    }

    public void SwapTiles(Tile oldTile, Tile newTile)
    {
        (oldTile.Material, newTile.Material) = (newTile.Material, oldTile.Material);
    }
}
