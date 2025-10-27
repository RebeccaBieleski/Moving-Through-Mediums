using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClicks : MonoBehaviour
{
    private InputAction click;

    void Awake()
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += OnMouseClick;
        click.Enable();
    }

    private void OnMouseClick(InputAction.CallbackContext ctx)
    {
        Vector3 coor = Mouse.current.position.ReadValue();
        foreach (var hit in Physics.RaycastAll(Camera.main.ScreenPointToRay(coor)))
        {
            hit.collider.GetComponent<IClickable>()?.OnClick();
        }
    }

    private void OnDestroy() => click.performed -= OnMouseClick;
}
