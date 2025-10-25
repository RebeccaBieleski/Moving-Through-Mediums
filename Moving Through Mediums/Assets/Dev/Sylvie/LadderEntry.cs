using UnityEngine;

public class LadderEntry : MonoBehaviour, IInteractable
{
    [SerializeField] LadderEntry NextLadderEntry;

    public void Interact(Character interactingCharacter) =>
        interactingCharacter.transform.position = NextLadderEntry.transform.position;
}
