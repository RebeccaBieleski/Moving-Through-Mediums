using UnityEngine;

public class TestToggleableSphere : MonoBehaviour, IToggleable
{
    public void Toggle()
    {
        var newScale = transform.localScale.x == 1 ? new Vector3(2, 2, 2) : new Vector3(1, 1, 1);
        transform.localScale = newScale;
    }
}
