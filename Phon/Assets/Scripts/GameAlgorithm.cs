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
    private int _gridWidth;
    private int _gridHeight;
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

        _gridWidth = GridManager.Instance.width;
        _gridHeight = GridManager.Instance.height;
    }

    internal void CheckMatch(Tile tile)
    {
        // CheckTPattern(tile);
        CheckLPattern(tile);
        // CheckHorizontal(tile);
        // CheckVertical(tile);
    }

    private void CheckLPattern(Tile tile)
    {
        // Zrobić sprawdzanie na każdym rzędzie

        _matchedTiles.Add(tile);

        for (int x = tile.Position.x + 1; x < _gridWidth; x++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(x, tile.Position.y));

            if (checkedTile.Material != tile.Material)
            {
                break;
            }

            horizontalTiles.Add(checkedTile);
        }

        for (int x = tile.Position.x - 1; x >= 0; x++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(x, tile.Position.y));

            if (checkedTile.Material != tile.Material)
            {
                break;
            }

            horizontalTiles.Add(checkedTile);
        }

        for (int y = tile.Position.y + 1; y < _gridHeight; y++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(tile.Position.x, y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }

            verticalTiles.Add(checkedTile);
        }
        RemoveTiles();
    }

    private void CheckTPattern(Tile tile)
    {
        _matchedTiles.Add(tile);
        List<Tile> leftTiles = new List<Tile>();
        List<Tile> rightTiles = new List<Tile>();
        List<Tile> upTiles = new List<Tile>();
        List<Tile> downTiles = new List<Tile>();

        for (int x = tile.Position.x; x > 0; x--)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(x, tile.Position.y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            leftTiles.Add(checkedTile);
        }
        
        for (int x = tile.Position.x; x < GridManager.Instance.width; x++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(x, tile.Position.y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            rightTiles.Add(checkedTile);
        }

        for (int y = tile.Position.y; y < GridManager.Instance.height; y++)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(tile.Position.x, y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            upTiles.Add(checkedTile);
        }
        
        for (int y = tile.Position.y; y > 0; y--)
        {
            Tile checkedTile = tilesStorage.GetTile(new Vector2(tile.Position.x, y));
            if (checkedTile.Material != tile.Material)
            {
                break;
            }
            downTiles.Add(checkedTile);
        }

        if (leftTiles.Count != rightTiles.Count)
        {
            return;
        }

        if (upTiles.Count != leftTiles.Count && downTiles.Count != leftTiles.Count)
        {
            return;
        }
        
        for (int i = 0; i < leftTiles.Count; i++)
        {
            _matchedTiles.Add(leftTiles[i]);   
            _matchedTiles.Add(rightTiles[i]);
        }

        if (upTiles.Count == leftTiles.Count)
        {
            for (int i = 0; i < leftTiles.Count; i++)
            {
                _matchedTiles.Add(upTiles[i]);   
            }
        }
        
        if (downTiles.Count == leftTiles.Count)
        {
            for (int i = 0; i < leftTiles.Count; i++)
            {
                _matchedTiles.Add(downTiles[i]);   
            }
        }
        RemoveTiles();
    }
    private void CheckHorizontal(Tile tile)
    {
        _matchedTiles.Add(tile);
        for (int i = tile.Position.x + 1; i < _gridWidth; i++)
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
        for (int i = tile.Position.y + 1; i < _gridHeight; i++)
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
            DropTiles();
            GridManager.Instance.FillGrid();
        }
        else
        {
            _matchedTiles.Clear();
        }
    }

    private void DropTiles()
    {
        for (int x = 0; x < GridManager.Instance.width; x++)
        {
            for (int y = 0; y < GridManager.Instance.height; y++)
            {
                Tile currentTile = tilesStorage.GetTile(new Vector2(x, y));
                if (currentTile != null && currentTile.Material == null)
                {
                    for (int i = y + 1; i < GridManager.Instance.height; i++)
                    {
                        Tile upperTile = tilesStorage.GetTile(new Vector2(x, i));
                        if (upperTile.Material != null)
                        {
                            currentTile.Material = upperTile.Material;
                            upperTile.Material = null;
                            break;
                        }
                    }
                }
            }
        }
    }
}
