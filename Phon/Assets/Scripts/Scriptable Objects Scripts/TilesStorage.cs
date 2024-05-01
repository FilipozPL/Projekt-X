using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Storage", menuName = "Scriptable Objects/Tile Storage", order = 2)]
public class TilesStorage : ScriptableObject
{
    public Dictionary<Vector2, Tile> tiles = new();

    public Tile GetTile(Vector2 position)
    {
        tiles.TryGetValue(position, out Tile tile);
        return tile;
    }

    public void SwapTiles(Tile oldTile, Tile newTile)
    {
        (oldTile.Material, newTile.Material) = (newTile.Material, oldTile.Material);
    }
}
