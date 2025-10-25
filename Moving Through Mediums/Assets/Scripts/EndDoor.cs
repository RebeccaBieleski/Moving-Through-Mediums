using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    [SerializeField] int NumberOfCharactersRequired = 3;

    private int _numberOfCharactersEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            Destroy(other.gameObject);
            _numberOfCharactersEntered++;

            if (_numberOfCharactersEntered >= NumberOfCharactersRequired)
            {
                EndLevel();
            }
        }
    }

    private void EndLevel()
    {
        Debug.Log("finish");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
