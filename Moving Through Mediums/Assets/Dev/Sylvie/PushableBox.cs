using UnityEngine;

public class PushableBox : MonoBehaviour, IInteractable
{
    [SerializeField] float PushForce = 100f;

    private Rigidbody _rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Interact(Vector2 direction)
    {
        _rigidBody.AddForce(PushForce * direction);
    }
}
