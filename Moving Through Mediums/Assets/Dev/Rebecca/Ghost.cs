using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{

    private InputAction InputPossess;

    private bool PossessingCurrently;

    private Character PosessedCharacter;

    private void Start()
    {
        InputPossess = InputSystem.actions.FindAction("Possess");
    }

    private void Update()
    {
        if (InputPossess.WasReleasedThisFrame() && PosessedCharacter != null) {
            Unpossess();
		}
    }

    private void Possess(InteractableCharacter character)
	{

	}

    private void Unpossess()
	{
        //Unpossess Character
        //Put Ghost back into game
	}

}
