using System;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] TilesStorage tilesStorage;
    GameObject currentTile;
    GameObject oldTile = null;
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
        // Vector2 position = Vector2Int.RoundToInt(mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y)));
        // oldTile = currentTile != null ? currentTile : null;
        // currentTile = gridManager.GetTile(position);
        // Debug.Log(oldTile.transform.position);
        // Debug.Log(currentTile.transform.position);
        // if (currentTile != null && oldTile != null)
        // {
        //     (oldTile.transform.position, currentTile.transform.position) = (currentTile.transform.position, oldTile.transform.position);
        //     currentTile = null;
        // }
        // oldTile = null;
    }
}
