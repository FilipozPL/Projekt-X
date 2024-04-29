using System;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilesStorage tilesStorage;
    Tile currentTile, oldTile = null;
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

    void OnTouch(EnhancedTouch.Finger finger)
    {
        Vector2 position = Vector2Int.RoundToInt(mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y)));
        oldTile = currentTile != null ? currentTile : null;
        currentTile = tilesStorage.GetTile(position);
        if (currentTile != null && oldTile != null)
        {
            var newMat = currentTile.Material;
            currentTile.Material = oldTile.Material;
            oldTile.Material = newMat;
            currentTile = null;
        }
        oldTile = null;
    }
}
