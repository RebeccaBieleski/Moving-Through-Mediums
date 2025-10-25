using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] public Direction Facing;

    public void UpdateFacing(Direction direction)
    {
        Facing = direction;
    }

    private IEnumerator DoDelayed(UnityAction action)
    {
        yield return null; // optional
        yield return new WaitForEndOfFrame(); // Wait for the next frame
        action.Invoke(); // execute a delegate
    }
}
