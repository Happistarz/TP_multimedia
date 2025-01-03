using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPlayerFollow : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;

    [SerializeField] private Transform player;
    [SerializeField] private float     sensitivity = 0.1f;

    private float _pitch;

    private void Start()
    {
        lookAction.action.Enable();
    }

    // LateUpdate is called once per frame after all Update functions have been called
    private void LateUpdate()
    {
        if (Time.timeScale == 0) return; // The game is paused, don't update the camera
        
        var look = lookAction.action.ReadValue<Vector2>();

        var yaw   = look.x * sensitivity;
        var pitch = look.y * sensitivity;

        _pitch -= pitch;
        _pitch =  Mathf.Clamp(_pitch, -45, 45);

        transform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        player.Rotate(Vector3.up, yaw);
    }
}