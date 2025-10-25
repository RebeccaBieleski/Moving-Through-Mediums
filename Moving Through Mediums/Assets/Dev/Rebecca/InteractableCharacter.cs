using UnityEngine;

public class InteractableCharacter : MonoBehaviour, IInteractable
{

	PlayerController attachedCharacter;

	public void Interact(Character interactingCharacter)
	{
		attachedCharacter.Possess();
	}
}