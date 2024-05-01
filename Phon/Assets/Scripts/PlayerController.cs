using System;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilesStorage tilesStorage;
    PlayerInput playerInput;
    Tile currentTile, oldTile = null;
    List<Tile> matchedTiles = new();
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        GameManager.OnGameStateChange += OnGameStateChange;
    }
    void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
    }
    void Start()
    {
        EnhancedTouch.Touch.onFingerDown += OnTouch;
        EnhancedTouch.Touch.onFingerMove += OnTouch;
    }
    void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
        EnhancedTouch.Touch.onFingerDown -= OnTouch;
        EnhancedTouch.Touch.onFingerMove -= OnTouch;
    }
    void OnGameStateChange(GameState state)
    {
        if(state == GameState.PlayerTurn)
        {
            playerInput.SwitchCurrentActionMap("Player");
        }
        else 
        {
            playerInput.SwitchCurrentActionMap("Disabled");
        }
    }
    void OnTouch(EnhancedTouch.Finger finger)
    {
        Vector2 position = Vector2Int.RoundToInt(mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y)));
        oldTile = currentTile != null ? currentTile : null;
        currentTile = tilesStorage.GetTile(position);
        if (currentTile != null && oldTile != null)
        {
            GameManager.Instance.UpdateGameState(GameState.GameTurn);
            tilesStorage.SwapTiles(oldTile, currentTile);
            CheckMatch(currentTile);
            CheckMatch(oldTile);
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
            currentTile = null;
        }
        oldTile = null;
    }
    void CheckMatch(Tile tile)
    {
        matchedTiles.Add(tile);
        var checkedTile = tilesStorage.GetTile(new Vector2Int(tile.Position.x + 1, tile.Position.y));
        if(checkedTile != null)
        {
            if(tile.Material == checkedTile.Material)
            {
                CheckMatch(checkedTile);
            }
            else
            {
                RemoveTiles();
            }
        }
        else
        {
            RemoveTiles();
        }
    }

    private void RemoveTiles()
    {
        if (matchedTiles.Count >= 3)
        {
            foreach (Tile matchedTile in matchedTiles)
            {
                Destroy(matchedTile.gameObject);
            }
            matchedTiles.Clear();
        }
        else
        {
            matchedTiles.Clear();
        }
    }
}
