using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableCube : MonoBehaviour, IClickable
{
    [SerializeField] CubeType CubeType = CubeType.REGULAR;
    [SerializeField] bool _isBeingControlled;

    private InputAction _touchInput;
    private Rigidbody _rigidBody;
    private Collider _cubeInteractRange;
    private Collider _cubeTelekinesisRange;
    private Collider _currentRangeToUse => _cubeTelekinesisRange != null
        ? _cubeTelekinesisRange 
            : _cubeInteractRange != null 
            ? _cubeInteractRange 
        : null;
    private Character _controlledBy;

    private bool PlayerIsInRange() => _cubeInteractRange != null || _cubeTelekinesisRange != null;

    private void Start()
    {
        _touchInput = InputSystem.actions.FindAction("Touch");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_controlledBy == null || !_controlledBy.UnderControl)
            return;

        if (_isBeingControlled)
        {
            var pointerPos = _touchInput.ReadValue<Vector2>();
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                pointerPos.x, pointerPos.y, Camera.main.transform.position.z * -1));
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);

            // check if out of bounds
            // theres probably a better way to keep a positon bound within an area
            var tRangeCollider = _currentRangeToUse;
            var newXPos = transform.localPosition.x;
            var newYPos = transform.localPosition.y;
            if (transform.localPosition.x > tRangeCollider.bounds.size.x / 2)
            {
                newXPos = tRangeCollider.bounds.size.x / 2;
            }
            if (transform.localPosition.x < tRangeCollider.bounds.size.x / -2)
            {
                newXPos = tRangeCollider.bounds.size.x / -2;
            }
            if (transform.localPosition.y > tRangeCollider.bounds.size.y / 2)
            {
                newYPos = tRangeCollider.bounds.size.y / 2;
            }
            if (transform.localPosition.y < tRangeCollider.bounds.size.y / -2)
            {
                newYPos = tRangeCollider.bounds.size.y / -2;
            }
            transform.localPosition = new Vector2(newXPos, newYPos);
        }
    }

    public void OnClick()
    {
        if (PlayerIsInRange())
        {
            var characterTryingToTakeBox = _currentRangeToUse.gameObject.GetComponentInParent<Character>();
            if (!characterTryingToTakeBox.UnderControl || _controlledBy != null && _controlledBy != characterTryingToTakeBox)
                return;

            _isBeingControlled = !_isBeingControlled;
            _rigidBody.isKinematic = _isBeingControlled;
            transform.parent = _isBeingControlled ? _currentRangeToUse.transform : null;
            _controlledBy = _isBeingControlled ? characterTryingToTakeBox : null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherInteractionRange = other.GetComponent<CharacterInteractionRange>();
        var otherTelekinesisRange = other.GetComponent<CharacterTelekinesisRange>();
        var otherCharacter = other.gameObject.GetComponentInParent<Character>();

        if (otherInteractionRange != null && (otherCharacter.CanMoveHeavy || CubeType != CubeType.HEAVY))
        {
            _cubeInteractRange = other;
        }
        else if (otherTelekinesisRange != null && CubeType == CubeType.REGULAR)
        {
            _cubeTelekinesisRange = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var otherInteractionRange = other.GetComponent<CharacterInteractionRange>();
        var otherTelekinesisRange = other.GetComponent<CharacterTelekinesisRange>();
        if (otherInteractionRange != null)
        {
            _cubeInteractRange = null;
        }
        else if (otherTelekinesisRange != null && CubeType == CubeType.REGULAR)
        {
            _cubeTelekinesisRange = null;
        }
    }
}
