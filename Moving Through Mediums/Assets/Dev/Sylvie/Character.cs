using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] float _heightWhenHeld = 1.5f;

    [SerializeField] public Direction Facing;

    private InteractableBox _heldBox;
    private InputAction _inputAction;

    private void Start()
    {
        _inputAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (_inputAction.WasReleasedThisFrame() && _heldBox != null)
        {
            ReleaseBox();
        }
    }

    public void PickupBox(InteractableBox box)
    {
        if (_heldBox != null)
            return;

        StartCoroutine(DoDelayed(() =>
        {
            _heldBox = box;
            _heldBox.DisablePhysics();
            _heldBox.gameObject.transform.parent = this.gameObject.transform;
            _heldBox.gameObject.transform.localPosition = new Vector3(0, _heightWhenHeld, 0);
        }));
    }

    public void ReleaseBox()
    {
        var xDisplacement = Facing == Direction.LEFT ? -1 : 1;
        _heldBox.gameObject.transform.localPosition = new Vector3(xDisplacement, _heightWhenHeld);

        _heldBox.EnablePhysics();
        _heldBox.gameObject.transform.parent = null;
        _heldBox = null;
    }

    public void UpdateFacing(Direction direction)
    {
        Facing = direction;
    }

    private IEnumerator DoDelayed(UnityAction action)
    {
        yield return null; // optional
        yield return new WaitForEndOfFrame(); // Wait for the next frame
        action.Invoke(); // execute a delegate
    }
}
