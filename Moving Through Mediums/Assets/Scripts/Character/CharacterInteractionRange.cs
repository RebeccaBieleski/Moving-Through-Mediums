using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractionRange : MonoBehaviour
{
    [SerializeField]
    private Character _currentCharacter;
    private InputAction _inputAction;
    private GameObject _currentInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
            _currentInteractable = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _currentInteractable)
            _currentInteractable = null;
    }

    private void Interact()
    {
        if (_inputAction.WasReleasedThisFrame() && _currentInteractable != null)
        {
            _currentInteractable.GetComponent<IInteractable>().Interact(_currentCharacter);
        }
    }
}
