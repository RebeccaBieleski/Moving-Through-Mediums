using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 30f;
    [SerializeField] float DragFactor = 0.85f;

    private InputAction _moveInput;
    private InputAction _jumpInput;
    private Rigidbody _rigidBody;
    private Character _character;

    private bool UnderControl = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveInput = InputSystem.actions.FindAction("Move");
        _rigidBody = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (UnderControl) {		

        Move();
        UpdateFacing();

        _rigidBody.linearVelocity = new Vector3(DragFactor * _rigidBody.linearVelocity.x,
            DragFactor * _rigidBody.linearVelocity.y,
            _rigidBody.linearVelocity.z);

        }
    }

    private void Move()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        var yMovementCommand = _moveInput.ReadValue<Vector2>().y;
        _rigidBody.AddForce(new Vector2(xMovementCommand * MoveSpeed, yMovementCommand * MoveSpeed));
    }

    private void UpdateFacing()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        if (xMovementCommand == 0)
            return;

        _character.UpdateFacing(xMovementCommand < 0 ? Direction.LEFT : Direction.RIGHT);
    }

    public void Possess ()
	{
        UnderControl = !UnderControl;

    }
}
