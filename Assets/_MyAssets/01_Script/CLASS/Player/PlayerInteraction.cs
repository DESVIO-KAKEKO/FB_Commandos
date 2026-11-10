using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]private Collider _trigger;
    private IInteractible _interactible;

    private void Awake()
    {
        if (_trigger == null) _trigger = GetComponent<Collider>();
        if (_trigger == null) this.enabled = false;
    }

    private void OnInteract()
    {
        if(_interactible != null)_interactible.Interact(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractible temp = other.GetComponentInParent<IInteractible>();
        if (temp != null)
        {
            if (temp.CanInteract(this.gameObject))
            {
                _interactible = temp;
                Debug.Log(_interactible);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _interactible = null;
    }
}
