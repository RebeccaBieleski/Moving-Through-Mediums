using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 50f;
    [SerializeField] float HorizontalDragFactor = 0.85f;

    private InputAction _inputAction;
    private Rigidbody _rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputAction = InputSystem.actions.FindAction("Move");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Interact();
    }

    private void FixedUpdate()
    {
        Move();

        _rigidBody.linearVelocity = new Vector3(HorizontalDragFactor * _rigidBody.linearVelocity.x,
            _rigidBody.linearVelocity.y,
            _rigidBody.linearVelocity.z);
    }

    private void Move()
    {
        var xMovementCommand = _inputAction.ReadValue<Vector2>().x;
        _rigidBody.AddForce(new Vector2(xMovementCommand * MoveSpeed, 0));
    }

    private void Interact()
    {

    }
}
