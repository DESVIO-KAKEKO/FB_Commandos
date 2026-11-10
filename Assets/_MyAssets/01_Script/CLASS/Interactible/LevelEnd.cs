using UnityEngine;
using UnityEngine.Events;

public class LevelEnd : MonoBehaviour, IInteractible
{
    public UnityEvent ExtractionReached;
    
    public bool CanInteract(GameObject interactor)
    {
        if(interactor.GetComponent<PlayerInteraction>() != null) return true;
        else return false;
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log("call end");
        ExtractionReached?.Invoke();
    }
}
