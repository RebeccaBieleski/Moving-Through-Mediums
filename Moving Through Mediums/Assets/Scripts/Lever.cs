using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] List<GameObject> ConnectedObjects = new();

    public void Interact(Character interactingCharacter)
    {
        if (!interactingCharacter.CanUseLever)
            return;

        foreach (var toggleable in ConnectedObjects)
        {
            toggleable.GetComponent<IToggleable>()?.Toggle();
        }
    }
}
