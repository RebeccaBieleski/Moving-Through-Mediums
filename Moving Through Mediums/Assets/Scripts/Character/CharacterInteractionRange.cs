using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractionRange : MonoBehaviour, IInteractionRange
{
    [SerializeField]
    private Character _currentCharacter;
    [SerializeField] Collider _collider;
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

    public void SetColliderActive(bool enable) => _collider.enabled = enable;

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
