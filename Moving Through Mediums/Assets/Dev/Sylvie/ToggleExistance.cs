using UnityEngine;

public class ToggleExistance : MonoBehaviour, IToggleable
{

    [SerializeField]
    private MeshRenderer Renderer;

    [SerializeField]
    private Collider Collider;

    public void Toggle()
    {
        Collider.enabled = !Collider.enabled;
        Renderer.enabled = !Renderer.enabled;
    }
}

