using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterFeet Feet;

    [SerializeField] public Direction Facing;
    [SerializeField] public bool CanUseLever;
    [SerializeField] public bool CanMoveHeavy;

    private IEntrance _currentEntrance;

    public void UpdateFacing(Direction direction)
    {
        Facing = direction;
    }

    public void UseEntrance(Direction direction)
    {
        if (_currentEntrance != null)
        {
            if (direction == Direction.UP)
            {
                _currentEntrance.MoveUp(this);
            }
            else if (direction == Direction.DOWN)
            {
                _currentEntrance.MoveDown(this);
            }
        } 
    }

    public void StickFeetToBox()
    {
        if (Feet.CubeUnderFeet != null)
        {
            transform.SetParent(Feet.CubeUnderFeet.transform, true);
        }
    }

    public void UnstickFeet()
    {
        transform.SetParent(null, true);
    }

    public bool IsGrounded() => Feet.Grounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEntrance>() != null)
            _currentEntrance = other.gameObject.GetComponent<IEntrance>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IEntrance>() == _currentEntrance)
            _currentEntrance = null;
    }

    private IEnumerator DoDelayed(UnityAction action)
    {
        yield return null; // optional
        yield return new WaitForEndOfFrame(); // Wait for the next frame
        action.Invoke(); // execute a delegate
    }
}
