using System.Collections.Generic;
using UnityEngine;

public class CharacterFeet : MonoBehaviour
{
    public ClickableCube CubeUnderFeet { get; private set; }
    public bool Grounded => _collidersTouchingFeet.Count > 0;

    private List<Collider> _collidersTouchingFeet = new();

    private void OnTriggerEnter(Collider other)
    {
        _collidersTouchingFeet.Add(other);

        if (other.gameObject.GetComponent<ClickableCube>() != null)
            CubeUnderFeet = other.gameObject.GetComponent<ClickableCube>();
    }

    private void OnTriggerExit(Collider other)
    {
        _collidersTouchingFeet.Remove(other);

        if (other.gameObject.GetComponent<ClickableCube>() == CubeUnderFeet)
            CubeUnderFeet = null;
    }
}
