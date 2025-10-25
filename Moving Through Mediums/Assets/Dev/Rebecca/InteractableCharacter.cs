using UnityEngine;

public class InteractableCharacter : MouseClicks, IInteractable
{

	PlayerController attachedCharacter;

	public void Interact(Character interactingCharacter)
	{
		attachedCharacter.Possess();
	}
}