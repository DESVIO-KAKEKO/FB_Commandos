using System;
using UnityEngine;

public class MatLamp : MonoBehaviour, IInteractible
{
    private bool _isOn = false;
    private MeshRenderer _renderer;

    [SerializeField]private Material _onMat;
    [SerializeField]private Material _offMat;

    [SerializeField]GameObject _enableEnd;

    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null) _renderer = gameObject.AddComponent<MeshRenderer>();
        
        if (_isOn) _renderer.material = _onMat;
        else _renderer.material = _offMat;
        
        _enableEnd.SetActive(false);
    }

    public bool CanInteract(GameObject interactor)
    {
        if(interactor.GetComponent<PlayerInteraction>() != null) return true;
        else return false;
    }

    public void Interact(GameObject interactor)
    {
        _isOn = !_isOn;
        if (_isOn)
        { 
            _renderer.material = _onMat;
            _enableEnd.SetActive(true);
        }
        else _renderer.material = _offMat;
    }
}
