using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class NewBehaviourScript : MonoBehaviour
{
    PlayerInput playerInput;
    // Start is called before the first frame update
    void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.TouchSimulation.Enable();
        playerInput = GetComponent<PlayerInput>();
        playerInput.SwitchCurrentControlScheme(InputSystem.devices.First(d => d == Touchscreen.current));
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
        Debug.Log("sus");
    }
}
