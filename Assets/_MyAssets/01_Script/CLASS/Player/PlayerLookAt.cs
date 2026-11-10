using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAt : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    
    private void OnLook(InputValue value)
    {
        Vector3 flatLookPosition = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
    }

}
