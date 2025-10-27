using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 30f;
    [SerializeField] float DragFactor = 0.85f;

    private InputAction _moveInput;
    private InputAction _jumpInput;
    private Rigidbody _rigidBody;
    [SerializeField]
    private Ghost _character;

    [SerializeField]
    public bool UnderControl = true;

    [SerializeField]
    private GameObject GhostRenderer;

    [SerializeField]
    private InputAction UnpossessInputAction;

    [SerializeField]
    private CapsuleCollider Collider;

    [SerializeField] private AudioSource PossessSfx;
    [SerializeField] private AudioSource UnpossessSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveInput = InputSystem.actions.FindAction("Move");
        _rigidBody = GetComponent<Rigidbody>();        


        UnpossessInputAction.performed += cty => {
            Possess(null);
        };
        UnpossessInputAction.Disable();
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

    public void Possess (PlayerController character)
	{
        UnderControl = !UnderControl;
        if (!UnderControl) {
            //Disappear Ghost
            GhostRenderer.transform.gameObject.SetActive(false);
            this.gameObject.transform.parent = character.transform;
            UnpossessInputAction.Enable();
            Collider.enabled = false;
            _rigidBody.isKinematic = true;
            PossessSfx.Play();
        } else {
            //Reappear Ghost
            GhostRenderer.transform.gameObject.SetActive(true);
            this.gameObject.transform.parent = null;
            UnpossessInputAction.Disable();
            Collider.enabled = true;
            _rigidBody.isKinematic = false;
            UnpossessSfx.Play();
        }
    }

}
