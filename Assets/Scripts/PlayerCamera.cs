using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    public float distance;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = player.transform.position + _offset * distance;
    }
}