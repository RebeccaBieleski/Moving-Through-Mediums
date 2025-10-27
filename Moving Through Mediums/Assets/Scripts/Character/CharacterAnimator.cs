using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Character _character;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(_character.Rigidbody.linearVelocity.x), 0.01f));
        _spriteRenderer.flipX = _character.Facing == Direction.LEFT;
    }
}
