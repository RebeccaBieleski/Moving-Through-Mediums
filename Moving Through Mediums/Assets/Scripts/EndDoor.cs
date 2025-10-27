using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour, IClickable
{
    [SerializeField] int NumberOfCharactersRequired = 3;

    private int _numberOfCharactersEntered;

    private Character _currentCharacterAtDoor;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInChildren<GhostController>();
        var character = other.GetComponent<Character>();
        if (player != null && character != null)
        {
            _currentCharacterAtDoor = character;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInChildren<GhostController>();
        var character = other.GetComponent<Character>();
        if (player != null && character != null)
        {
            _currentCharacterAtDoor = null;
        }
    }

    public void OnClick()
    {
        if (_currentCharacterAtDoor != null)
        {
            _currentCharacterAtDoor.GetComponentInChildren<GhostController>().Possess(null); // unpossess them
            _currentCharacterAtDoor.transform.position = new Vector3(0, 0, 100);
            _currentCharacterAtDoor = null;
            _numberOfCharactersEntered++;

            if (_numberOfCharactersEntered >= NumberOfCharactersRequired)
            {
                EndLevel();
            }
        }
    }

    private void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
