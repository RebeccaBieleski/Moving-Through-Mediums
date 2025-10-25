using UnityEngine;
using UnityEngine.InputSystem;

public class GhostInteractionPointer : MonoBehaviour
{
    [SerializeField]
    private GhostController GhostCharacter;
    [SerializeField]
    private InputAction PossessInputAction;
    private PlayerController _currentInteractable;

    [SerializeField]
    private float possessionDistance = 5;

    // Update is called once per frame
    void Update()
    {
        if (GhostCharacter.UnderControl)
        CheckPossessionInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
            _currentInteractable = other.gameObject.GetComponent<PlayerController>();
        else
            _currentInteractable = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _currentInteractable)
            _currentInteractable = null;
    }

    private void CheckPossessionInput()
    {
        if (PossessInputAction.WasReleasedThisFrame() && _currentInteractable != null) {
            //Check if target is in range
            if (Vector3.Distance(GhostCharacter.transform.position, _currentInteractable.transform.position) < possessionDistance){
                _currentInteractable.Possess();
                GhostCharacter.Possess();
            }            
        }
    }
}
