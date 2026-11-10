using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAt : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _stickLook;
    private InputSystem_Actions _input;

    private bool _usingGamepad = false;

    private void Awake()
    {
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (_mainCamera == null) this.enabled = false;

        _input = new InputSystem_Actions();
        _input.Player.Look.performed += Onlook;
    }

    private void OnEnable()
    {
        _input.Player.Look.performed += Onlook;
        _input.Enable();
    }
    
    private void OnDisable()
    {
        _input.Player.Look.performed -= Onlook;
        _input.Disable();
    }

    private void Onlook(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is Gamepad)
        {
            _usingGamepad = true;
            _stickLook = ctx.ReadValue<Vector2>();
        }
        else _usingGamepad = false;
    }

    private void Update()
    {
        Vector3 direction;

        if (_usingGamepad) direction = new Vector3(_stickLook.x, 0, _stickLook.y);
        else if (Mouse.current != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane groundPlane = new Plane(Vector3.up, transform.position);

            if (!groundPlane.Raycast(ray, out float distance)) return;
            
            Vector3 hitPoint = ray.GetPoint(distance);
            direction = hitPoint - transform.position;
            direction.y = 0f;
        }
        else return;

        if (direction.sqrMagnitude < 0.0001f) return;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
