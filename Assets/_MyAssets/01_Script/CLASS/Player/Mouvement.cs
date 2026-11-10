using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouvement : MonoBehaviour
{
    [SerializeField] float _speed = 5.0f;
    private Rigidbody _rb;
    private Vector3 _mouvementDirection = Vector3.zero;
    
    private void Awake()
    {
        if(_rb == null) _rb = GetComponent<Rigidbody>();
        if (_rb == null) _rb = gameObject.AddComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputMouvement = Vector2.ClampMagnitude(value.Get<Vector2>(),1f);
        _mouvementDirection = new Vector3(inputMouvement.x, 0, inputMouvement.y);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _mouvementDirection * _speed * Time.fixedDeltaTime);
    }
}
