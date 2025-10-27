using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] List<GameObject> ConnectedObjects = new();

    [SerializeField] private Sprite OffSprite;

    [SerializeField] private Sprite OnSprite;

    [SerializeField] private SpriteRenderer Renderer;

    [SerializeField] private AudioSource OnSfx;

    [SerializeField] private AudioSource OffSfx;

    public void Interact(Character interactingCharacter)
    {
        if (!interactingCharacter.CanUseLever)
            return;

        foreach (var toggleable in ConnectedObjects)
        {
            toggleable.transform.gameObject.SetActive(!toggleable.transform.gameObject.activeInHierarchy);
        }

        if (Renderer.sprite == OffSprite) {
            Renderer.sprite = OnSprite;
            OnSfx.Play();
        } else {
            Renderer.sprite = OffSprite;
            OffSfx.Play();
        }
    }
}
