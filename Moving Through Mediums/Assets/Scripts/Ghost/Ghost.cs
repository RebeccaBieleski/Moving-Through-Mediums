using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] public Direction Facing;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void UpdateFacing(Direction direction)
    {
        Facing = direction;
        _spriteRenderer.flipX = direction == Direction.LEFT;
    }
}
