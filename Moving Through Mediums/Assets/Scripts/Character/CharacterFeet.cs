using UnityEngine;

public class CharacterFeet : MonoBehaviour
{
    public ClickableCube CubeUnderFeet { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ClickableCube>() != null)
            CubeUnderFeet = other.gameObject.GetComponent<ClickableCube>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ClickableCube>() == CubeUnderFeet)
            CubeUnderFeet = null;
    }
}
