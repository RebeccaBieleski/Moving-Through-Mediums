using UnityEngine;
using UnityEngine.InputSystem;

public class ClickableCube : MonoBehaviour, IClickable
{
    [SerializeField] bool _isUnderTelekinesis;

    private InputAction _touchInput;
    private Rigidbody _rigidBody;

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
            gameObject.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
        }
    }

    public void OnClick()
    {
        _isUnderTelekinesis = !_isUnderTelekinesis;
        _rigidBody.isKinematic = _isUnderTelekinesis;
    }
}
