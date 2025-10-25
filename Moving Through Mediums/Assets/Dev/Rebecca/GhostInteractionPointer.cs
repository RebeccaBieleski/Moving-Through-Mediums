using UnityEngine;
using UnityEngine.InputSystem;

public class GhostInteractionPointer : MonoBehaviour
{
    [SerializeField]
    private GhostController GhostCharacter;
    [SerializeField]
    private InputAction PossessInputAction;
    private PlayerController _currentInteractable;

    // Update is called once per frame
    void Update()
    {
        CheckPossessionInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null && other.gameObject.GetComponent<PlayerController>())
            _currentInteractable = other.gameObject.GetComponent<PlayerController>();
        else
            _currentInteractable = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _currentInteractable)
            _currentInteractable = null;
    }


    private void CheckPossessionInput()
    {
        if (PossessInputAction.WasReleasedThisFrame() && _currentInteractable != null) {
            _currentInteractable.Possess();
            GhostCharacter.Possess();
        }
    }
}
