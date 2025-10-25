using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClicks : MonoBehaviour
{
    private InputAction click;

    void Awake()
    {
        click = new InputAction(binding: "<Mouse>/leftButton");
        click.performed += ctx => {
            Vector3 coor = Mouse.current.position.ReadValue();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(coor), out RaycastHit hit))
            {
                hit.collider.GetComponent<IClickable>()?.OnClick();
            }
        };
        click.Enable();
    }
}
