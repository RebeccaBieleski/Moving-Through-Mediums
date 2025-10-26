using UnityEngine;

public class CharacterTelekinesisRange : MonoBehaviour, IInteractionRange
{
    [SerializeField] Collider _collider;

    public void SetColliderActive(bool enable) => _collider.enabled = enable;
}
