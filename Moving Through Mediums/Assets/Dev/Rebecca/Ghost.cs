using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{

    [SerializeField]
    private InputAction UnpossessInput;

    private bool PossessingCurrently;

    private Character PosessedCharacter;

    [SerializeField] public Direction Facing;

    private void Start()
    {
    }

    private void Update()
    {
        if (UnpossessInput.WasReleasedThisFrame() && PosessedCharacter != null) {
            Unpossess();
		}
    }

    private void Possess(InteractableCharacter character)
	{

	}

    public void UpdateFacing(Direction direction)
    {
        Facing = direction;
    }

    private void Unpossess()
	{
        //Unpossess Character
        //Put Ghost back into game
	}

}
