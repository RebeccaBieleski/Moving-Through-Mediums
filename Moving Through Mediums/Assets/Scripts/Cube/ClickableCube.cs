using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableCube : MonoBehaviour, IClickable
{
    [SerializeField] CubeType CubeType = CubeType.HEAVY;
    [SerializeField] bool _isBeingControlled;
    [SerializeField] Collider _cubeControlRange;

    private InputAction _touchInput;
    private Rigidbody _rigidBody;
    private bool PlayerIsInRange() => _cubeControlRange != null;

    private void Start()
    {
        _touchInput = InputSystem.actions.FindAction("Touch");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isBeingControlled)
        {
            var pointerPos = _touchInput.ReadValue<Vector2>();
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                pointerPos.x, pointerPos.y, Camera.main.transform.position.z * -1));
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);

            // check if out of bounds
            // theres probably a better way to keep a positon bound within an area
            var tRangeCollider = _cubeControlRange;
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
            _isBeingControlled = !_isBeingControlled;
            _rigidBody.isKinematic = _isBeingControlled;
            transform.parent = _isBeingControlled ? _cubeControlRange.transform : null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((CubeType == CubeType.HEAVY && other.gameObject.GetComponent<CharacterInteractionRange>() != null && other.gameObject.GetComponentInParent<Character>().CanMoveHeavy)
            || (CubeType == CubeType.TELEKINETIC && other.gameObject.GetComponent<CharacterTelekinesisRange>() != null))
        {
            _cubeControlRange = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((CubeType == CubeType.HEAVY && other.gameObject.GetComponent<CharacterInteractionRange>() != null)
            || (CubeType == CubeType.TELEKINETIC && other.gameObject.GetComponent<CharacterTelekinesisRange>() != null))
        {
            _cubeControlRange = null;
        }
    }
}
