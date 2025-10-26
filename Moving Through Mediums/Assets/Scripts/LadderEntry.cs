using UnityEngine;

public class LadderEntry : MonoBehaviour, IEntrance
{
    [SerializeField] LadderEntry UpExit;
    [SerializeField] LadderEntry DownExit;

    public void MoveUp(Character character)
    {
        if (UpExit != null)
            character.transform.position = UpExit.transform.position;
    }

    public void MoveDown(Character character)
    {
        if (DownExit != null)
            character.transform.position = DownExit.transform.position;
    }
}
