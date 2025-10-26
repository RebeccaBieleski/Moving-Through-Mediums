using UnityEngine;
using UnityEngine.InputSystem;

public class GhostInteractionPointer : MonoBehaviour
{
    [SerializeField]
    private GhostController GhostCharacter;    
    [SerializeField]
    private PlayerController currentMousedOverCharacter;

    [SerializeField]
    private float possessionDistance = 50;

    [SerializeField]
    private InputAction PossessInputAction;    

    private void Awake()
	{
        PossessInputAction.performed += ctx => {
                Vector3 coor = Mouse.current.position.ReadValue();
                foreach (var hit in Physics.RaycastAll(Camera.main.ScreenPointToRay(coor))) {
                    currentMousedOverCharacter = hit.collider.GetComponent<PlayerController>();
                    if (currentMousedOverCharacter != null) {
                    CheckPossessionInput();
                    break;
                    }
                }
            };
        PossessInputAction.Enable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
            currentMousedOverCharacter = other.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentMousedOverCharacter)
            currentMousedOverCharacter = null;
    }

    private void CheckPossessionInput()
    {
        if (GhostCharacter.UnderControl) {
                //Check if target is in range
            if (Vector3.Distance(GhostCharacter.transform.position, currentMousedOverCharacter.transform.position) < possessionDistance) {
                currentMousedOverCharacter.Possess();
                GhostCharacter.Possess(currentMousedOverCharacter);
            }
            
        }
    }
}
