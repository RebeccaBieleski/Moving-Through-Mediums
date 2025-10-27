using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    private InputAction _resetAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _resetAction = InputSystem.actions.FindAction("ResetLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if (_resetAction.WasReleasedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
