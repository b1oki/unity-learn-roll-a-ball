using UnityEngine;

public class Rotator : MonoBehaviour
{
    private readonly float RotationSpeed = 15;

    void Update()
    {
        transform.Rotate(Time.deltaTime * RotationSpeed * new Vector3(0, 0, 15));
    }
}