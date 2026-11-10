using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _positionOffset = new Vector3(0,7,0);
    [SerializeField] float _smoothTime = .12f;
    
    private Camera _camera;
    private Vector3 _newPosition;
    private Vector3 _viewportPosition = Vector3.zero;
    float _followVelocityX;
    float _followVelocityZ;

    private void Awake()
    {
        if (_camera == null) _camera = GetComponent<Camera>();
        if(_camera == null) _camera = gameObject.AddComponent<Camera>();
    }
    
    private void LateUpdate()
    {
        _newPosition.y = _positionOffset.y;
        _viewportPosition = _camera.WorldToViewportPoint(_target.position);
        if (_viewportPosition.x < .3f || _viewportPosition.x > .7f)
        {
            _newPosition.x = Mathf.SmoothDamp(
                transform.position.x, 
                _target.position.x + _positionOffset.x,
                ref _followVelocityX, 
                _smoothTime, 
                Mathf.Infinity,
                Time.deltaTime
            ); 
        }
        if (_viewportPosition.y < .4f || _viewportPosition.y > .6f)
        {
            _newPosition.z = Mathf.SmoothDamp(
                transform.position.z, 
                _target.position.z + _positionOffset.z,
                ref _followVelocityZ, 
                _smoothTime,
                Mathf.Infinity,
                Time.deltaTime
            );
        } 
        transform.position = _newPosition;
    }
}
