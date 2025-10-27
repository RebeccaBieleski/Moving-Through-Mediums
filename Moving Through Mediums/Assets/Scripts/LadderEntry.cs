using UnityEngine;

public class LadderEntry : MonoBehaviour, IEntrance
{
    [SerializeField] LadderEntry UpExit;
    [SerializeField] LadderEntry DownExit;
    [SerializeField] AudioSource StairSfx;

    public void MoveUp(Character character)
    {
        if (UpExit != null) {
            character.transform.position = UpExit.transform.position;
            StairSfx.Play();
        }
    }

    public void MoveDown(Character character)
    {
        if (DownExit != null) { 
            character.transform.position = DownExit.transform.position;
            StairSfx.Play();
        }
    }
}
