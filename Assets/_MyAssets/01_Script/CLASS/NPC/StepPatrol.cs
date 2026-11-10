using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StepPatrol : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] Transform[] _waypoints;
    private float _pauseTime = 1.5f;
    private int _targetIndex = 0;
    private float _rotSpeed = 2;

    private float _patrolSpeed = 3.5f;
    private float _chaseSpeed = 5f;

    [SerializeField] ConeDetection _detector;
    
    private void Awake()
    {
        if (_agent == null) _agent = GetComponent<NavMeshAgent>();
        if (_agent == null) _agent = gameObject.AddComponent<NavMeshAgent>();

        if (_detector == null) _detector = GetComponent<ConeDetection>();
        if (_detector == null) _detector = gameObject.AddComponent<ConeDetection>();
        
        StartCoroutine(PatrolLoop());
    }

    private void Update()
    {
        if(_detector.GetDetectionState())
        {
            StopCoroutine(PatrolLoop());
            StartCoroutine(FollowPlayer());
        }
    }

    private IEnumerator PatrolLoop()
    {
        _agent.speed = _patrolSpeed;
        if(!_agent.pathPending && _agent.remainingDistance < .25f)
        {
            if (_waypoints.Length <= 0) yield break;
            _agent.destination = _waypoints[_targetIndex].position;
        }
        yield return new WaitForSeconds(_pauseTime);
        _targetIndex++;
        if (_targetIndex > _waypoints.Length-1) _targetIndex = 0;
        StartCoroutine(PatrolLoop());
    }

    private IEnumerator FollowPlayer()
    {
        _agent.speed = _chaseSpeed;
        if (!_agent.pathPending && _agent.remainingDistance < .25f)
        {
            _agent.destination = _detector.GetTargetPosition();
        }
        yield return new WaitForSeconds(_pauseTime);
    }

    private IEnumerator CheckArround()
    {
        Vector3 rotAxis = new Vector3(0, 1, 0);
        transform.Rotate(rotAxis, Mathf.Sin(Time.deltaTime*_rotSpeed));
        StartCoroutine(PatrolLoop());
        yield return null;
    }
}
