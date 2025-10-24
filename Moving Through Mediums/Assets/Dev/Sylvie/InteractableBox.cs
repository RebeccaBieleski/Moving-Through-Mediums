using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Interact(Character interactingCharacter)
    {
        interactingCharacter.PickupBox(this);
    }

    public void DisablePhysics()
    {
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
    }

    public void EnablePhysics()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
    }
}
