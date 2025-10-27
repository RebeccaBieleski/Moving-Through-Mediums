using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void GoToNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
