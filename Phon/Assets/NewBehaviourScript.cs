using System;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GridManager gridManager;
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
        Vector2 position = Vector2Int.RoundToInt(mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y)));
        oldTile = currentTile != null ? currentTile : null;
        currentTile = gridManager.GetTile(position);
        if (currentTile != null && oldTile != null)
        {
            Debug.Log(oldTile.transform.position);
            currentTile = gridManager.GetTile(position);
            Debug.Log(currentTile.transform.position);
            (oldTile.transform.position, currentTile.transform.position) = (currentTile.transform.position, oldTile.transform.position);
            oldTile = null;
            currentTile = null;
        }
    }
}
