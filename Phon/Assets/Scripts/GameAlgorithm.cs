using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameAlgorithm : MonoBehaviour
{
    public static GameAlgorithm Instance { get; private set; }
    [SerializeField] private TilesStorage tilesStorage;
    private readonly List<Tile> _matchedTiles = new();

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

    internal void CheckMatch(Tile tile)
    {
        CheckHorizontal(tile);
        CheckVertical(tile);
        
    }

    private void CheckHorizontal(Tile tile)
    {
        _matchedTiles.Add(tile);
        for (int i = tile.Position.x + 1; i < GridManager.Instance.width; i++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2Int(i, tile.Position.y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            _matchedTiles.Add(checkedTile);
        }
        
        for (int i = tile.Position.x - 1; i >= 0; i--)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2Int(i, tile.Position.y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            _matchedTiles.Add(checkedTile);
        }
        RemoveTiles();
    }

    private void CheckVertical(Tile tile)
    {
        _matchedTiles.Add(tile);
        for (int i = tile.Position.y + 1; i < GridManager.Instance.width; i++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2Int(tile.Position.x, i));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            _matchedTiles.Add(checkedTile);
        }
        
        for (int i = tile.Position.y - 1; i >= 0; i--)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2Int(tile.Position.x, i));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            _matchedTiles.Add(checkedTile);
        }
        RemoveTiles();
    }
    
    private void RemoveTiles()
    {
        if (_matchedTiles.Count >= 3)
        {
            foreach (Tile matchedTile in _matchedTiles)
            {
                matchedTile.Material = null;
            }
            _matchedTiles.Clear();
        }
        else
        {
            _matchedTiles.Clear();
        }
    }
}
