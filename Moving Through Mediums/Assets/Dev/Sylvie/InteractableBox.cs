using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact(Character interactingCharacter)
    {
        interactingCharacter.PickupBox(this);
    }

    public void DisablePhysics() => _rigidbody.isKinematic = true;
    public void EnablePhysics() => _rigidbody.isKinematic = false;
}
