using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    // Start is called before the first frame update
    void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
    }
    void Start()
    {
        EnhancedTouch.Touch.onFingerDown += OnTouch;
    }
    void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.TouchSimulation.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTouch(EnhancedTouch.Finger finger)
    {
        gameObject.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(finger.screenPosition.x, finger.screenPosition.y, 10));
    }
}
