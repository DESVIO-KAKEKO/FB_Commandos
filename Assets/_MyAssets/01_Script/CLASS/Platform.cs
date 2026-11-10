using UnityEngine;

public class Platform : MonoBehaviour
{ 
    [SerializeField] private Vector3 _mouvementVector = Vector3.zero;
    private Vector3 _startPosition;
    private float _distance = 15.0f;
    private float _speed = 5.0f;
    void Start()
    { 
        _startPosition = transform.position;
    }
    void Update()
    { 
        float offset = Mathf.Sin(Time.time * _speed) * _distance; 
        transform.position = _startPosition + _mouvementVector * offset; 
    }
}

