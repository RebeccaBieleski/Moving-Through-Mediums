using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    public Direction Direction = Direction.UP;

    public void Interact(Character interactingCharacter)
    {
        Direction = Direction == Direction.UP ? Direction.DOWN : Direction.UP;
    }
}
