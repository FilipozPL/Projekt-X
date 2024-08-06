using System;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TilesStorage tilesStorage;
    private PlayerInput _playerInput;
    private Tile _currentTile, _oldTile = null;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        GameManager.OnGameStateChange += OnGameStateChange;
    }
    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
    }
    private void Start()
    {
        EnhancedTouch.Touch.onFingerDown += OnTouch;
        EnhancedTouch.Touch.onFingerMove += OnTouch;
    }
    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
        EnhancedTouch.Touch.onFingerDown -= OnTouch;
        EnhancedTouch.Touch.onFingerMove -= OnTouch;
    }
    private void OnGameStateChange(GameState state)
    {
        _playerInput.SwitchCurrentActionMap(state == GameState.PlayerTurn ? "Player" : "Disabled");
    }
    private void OnTouch(EnhancedTouch.Finger finger)
    {
        // Get touch position and round it to int
        Vector2 position = Vector2Int.RoundToInt(mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y)));
        // If touched on tile first time, then make it active
        _oldTile = _currentTile != null ? _currentTile : null;
        _currentTile = tilesStorage.GetTile(position);
        if (_currentTile != null && _oldTile != null)
        {
            if (Vector2Int.Distance(_currentTile.Position, _oldTile.Position) > 1)
            {
                _currentTile = null;
                _oldTile = null;
                return;
            }
            GameManager.Instance.UpdateGameState(GameState.GameTurn);
            tilesStorage.SwapTiles(_oldTile, _currentTile);
            GameAlgorithm.Instance.CheckMatch(_currentTile);
            GameAlgorithm.Instance.CheckMatch(_oldTile);
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
            _currentTile = null;
        }
        _oldTile = null;
    }
}
