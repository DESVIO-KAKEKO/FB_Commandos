using System;
using UnityEngine;

public class Door : MonoBehaviour, IInteractible
{
    private Animator _animControl;
    private bool _isOpen = false;

    private float _current;
    private float _target;
    
    private float _animSpeed = .5f;
    private float _velocity = .0f;

    private void Awake()
    {
        if (_animControl == null) _animControl = GetComponent<Animator>();
        if (_animControl == null) this.enabled = false;
    }

    public bool CanInteract(GameObject interactor)
    {
        if(interactor.GetComponent<PlayerInteraction>() != null) return true;
        else return false;
    }

    public void Interact(GameObject interactor)
    {
        _isOpen = !_isOpen;
    }

    private void Update()
    {
        if (_isOpen) _target = 1f;
        else _target = 0f;
        
        _current = _animControl.GetFloat("Door_State");
        _animControl.SetFloat("Door_State", Mathf.SmoothDamp(_current,_target, ref _velocity, _animSpeed));
    }
}
