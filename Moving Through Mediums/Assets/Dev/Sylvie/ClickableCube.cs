using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableCube : MonoBehaviour, IClickable
{
    [SerializeField] bool _isUnderTelekinesis;
    [SerializeField] PlayerTelekinesisPointer _playerTelekinesisRange;

    private InputAction _touchInput;
    private Rigidbody _rigidBody;
    private bool PlayerIsInRange() => _playerTelekinesisRange != null;

    private void Start()
    {
        _touchInput = InputSystem.actions.FindAction("Touch");
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isUnderTelekinesis)
        {
            var pointerPos = _touchInput.ReadValue<Vector2>();
            var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                pointerPos.x, pointerPos.y, Camera.main.transform.position.z * -1));
            transform.position = new Vector3(worldPos.x, worldPos.y, 0);

            // check if out of bounds
            // theres probably a better way to keep a positon bound within an area
            var tRangeCollider = _playerTelekinesisRange.GetComponent<Collider>();
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
            if (transform.localPosition.y > tRangeCollider.bounds.size.y)
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
            _isUnderTelekinesis = !_isUnderTelekinesis;
            _rigidBody.isKinematic = _isUnderTelekinesis;
            transform.parent = _playerTelekinesisRange.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerTelekinesisPointer>() != null)
            _playerTelekinesisRange = other.gameObject.GetComponent<PlayerTelekinesisPointer>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerTelekinesisPointer>() != null)
        {
            transform.parent = null;
            _playerTelekinesisRange = null;
        }
    }
}
