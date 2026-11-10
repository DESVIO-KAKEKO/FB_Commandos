using System;
using UnityEditor.UI;
using UnityEngine;

public class ConeDetection : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField][Range(1,360)] private float _fov = 90f;
    [SerializeField]private float _maxRange = 5f;
    private float _viewHeight = .5f;
    private Vector3 _viewOffset = new Vector3();

    [SerializeField] private LayerMask _obstacleMask;
    
    Color debugColor = Color.red;
    
    private float _cosHalfFov;

    private bool _isSeen = false;
    private Vector3 _lastPlayerPos = new Vector3();
    
    private void Awake()
    {
        if (_target == null) _target = GameObject.FindWithTag("Player").transform;
        if (_target == null) this.enabled = false;
        
        _cosHalfFov = Mathf.Cos(_fov * 0.5f * Mathf.Deg2Rad);

        _viewOffset.y = _viewHeight;
    }

    private void Update()
    {
        _isSeen = IsDetecting(_target);
        if (_isSeen)
        {
            Debug.Log("player seen ! ");
        }
    }

    private bool IsDetecting(Transform target)
    {
        Vector3 distance = target.position - transform.position;

        Vector3 flatToTarget = new Vector3(distance.x, 0, distance.z);
        Vector3 flatForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        
        bool withinRange = flatToTarget.sqrMagnitude <= _maxRange * _maxRange;
        if (withinRange)
        {
            bool insideAngle = Vector3.Dot(flatForward.normalized, flatToTarget.normalized) >= _cosHalfFov;
            if (insideAngle)
            {
                Vector3 direction = target.position - (transform.position + _viewOffset);

                if (direction.sqrMagnitude <= _maxRange * _maxRange)
                {
                    Debug.DrawRay(transform.position + _viewOffset, direction, color:Color.softYellow);
                    debugColor = Color.softYellow;
                
                    RaycastHit hit;

                    if (!Physics.Raycast(transform.position + _viewOffset, direction, out hit, direction.magnitude, _obstacleMask))
                    {
                        Debug.DrawRay(transform.position + _viewOffset, direction, color: Color.red);
                        debugColor = Color.red;
                        _lastPlayerPos = _target.position;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else debugColor = Color.green;

            return false;
        }
        else
        {
            debugColor = Color.green;
            return false;
        } 
        
    }

    public bool GetDetectionState()
    {
        return _isSeen;
    }

    public Vector3 GetTargetPosition()
    {
        return _lastPlayerPos;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(_fov *0.5f, Vector3.up) * transform.forward * _maxRange,debugColor);
        Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-_fov *0.5f, Vector3.up) * transform.forward * _maxRange,debugColor);
        Gizmos.color = debugColor;
        Gizmos.DrawWireSphere(transform.position, _maxRange);
    }
}