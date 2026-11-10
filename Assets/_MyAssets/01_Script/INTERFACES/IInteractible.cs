using UnityEngine;

public interface IInteractible
{
    bool CanInteract(GameObject interactor);
    void Interact(GameObject interactor);
}
