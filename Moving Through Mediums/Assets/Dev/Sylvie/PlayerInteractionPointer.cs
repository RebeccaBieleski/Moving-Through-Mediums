using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionPointer : MonoBehaviour
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
        UpdatePosition();
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

    private void UpdatePosition()
    {
        transform.localPosition = _currentCharacter.Facing == Direction.LEFT ? Vector3.left : Vector3.right;
    }

    private void Interact()
    {
        if (_inputAction.WasReleasedThisFrame() && _currentInteractable != null)
        {
            _currentInteractable.GetComponent<IInteractable>().Interact(_currentCharacter);
        }
    }
}
