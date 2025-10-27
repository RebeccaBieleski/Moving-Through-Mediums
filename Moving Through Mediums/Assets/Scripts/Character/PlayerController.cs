using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 30f;
    [SerializeField] float JumpForce = 5f;
    [SerializeField] float HorizontalDragFactor = 0.85f;

    private InputAction _moveInput;
    private InputAction _jumpInput;
    private InputAction _upInput;
    private InputAction _downInput;
    private Rigidbody _rigidBody;
    private Character _character;
    private List<IInteractionRange> _childInteractionRanges;

    [SerializeField]
    private GhostController GhostPrefab;

    [SerializeField]
    public bool UnderControl = false;

    [SerializeField]
    private InputAction UnpossessInputAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveInput = InputSystem.actions.FindAction("Move");
        _jumpInput = InputSystem.actions.FindAction("Jump");
        _upInput = InputSystem.actions.FindAction("Up");
        _downInput = InputSystem.actions.FindAction("Down");
        _rigidBody = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();
        _childInteractionRanges = GetComponentsInChildren<IInteractionRange>().ToList();

        UnpossessInputAction.performed += ctx => Possess();
        UnpossessInputAction.Disable();
    }

    private void Update()
    {
        if (UnderControl)
        {
            Jump();
            EnterEntrance();
        }
    }

    private void FixedUpdate()
    {
        if (UnderControl) 
        {
            Move();
            UpdateFacing();

            _rigidBody.linearVelocity = new Vector3(HorizontalDragFactor * _rigidBody.linearVelocity.x,
                _rigidBody.linearVelocity.y,
                _rigidBody.linearVelocity.z);
        }
    }

    private void Move()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        _rigidBody.AddForce(new Vector2(xMovementCommand * MoveSpeed, 0));
    }

    private void Jump()
    {
        if (_jumpInput.triggered && _character.IsGrounded())
        {
            _rigidBody.AddForce(new Vector2(0, JumpForce), ForceMode.Impulse);
        }
    }

    private void EnterEntrance()
    {
        if (_upInput.WasReleasedThisFrame())
        {
            _character.UseEntrance(Direction.UP);
        }
        else if (_downInput.WasReleasedThisFrame())
        {
            _character.UseEntrance(Direction.DOWN);
        }
    }

    private void UpdateFacing()
    {
        var xMovementCommand = _moveInput.ReadValue<Vector2>().x;
        if (xMovementCommand == 0)
            return;

        _character.UpdateFacing(xMovementCommand < 0 ? Direction.LEFT : Direction.RIGHT);
    }

    public void Possess()
	{
        UnderControl = !UnderControl;
        _character.SetPossessed(UnderControl);
        if (UnderControl) 
        {
            UnpossessInputAction.Enable();
            foreach (var r in _childInteractionRanges)
                r.SetColliderActive(true);
        } 
        else 
        {
            UnpossessInputAction.Disable();
            foreach (var r in _childInteractionRanges)
                r.SetColliderActive(false);
        }
    }
}
