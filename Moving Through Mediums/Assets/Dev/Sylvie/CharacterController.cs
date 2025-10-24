using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 30f;
    [SerializeField] float JumpForce = 5f;
    [SerializeField] float HorizontalDragFactor = 0.85f;

    private InputAction _moveInput;
    private InputAction _jumpInput;
    private Rigidbody _rigidBody;
    private Character _character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveInput = InputSystem.actions.FindAction("Move");
        _jumpInput = InputSystem.actions.FindAction("Jump");
        _rigidBody = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        UpdateFacing();

        _rigidBody.linearVelocity = new Vector3(HorizontalDragFactor * _rigidBody.linearVelocity.x,
            _rigidBody.linearVelocity.y,
            _rigidBody.linearVelocity.z);
    }

    private void Move()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        _rigidBody.AddForce(new Vector2(xMovementCommand * MoveSpeed, 0));
    }

    private void Jump()
    {
        if (_jumpInput.triggered)
        {
            _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);
        }
    }

    private void UpdateFacing()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        if (xMovementCommand == 0)
            return;

        _character.UpdateFacing(xMovementCommand < 0 ? Direction.LEFT : Direction.RIGHT);
    }
}
