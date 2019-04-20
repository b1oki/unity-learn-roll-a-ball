using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxJumpHeight;
    public Text scoresText;

    private Rigidbody _rb;
    private int _scoresCount;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _scoresCount = 0;
        SetScoresText();
    }

    private void FixedUpdate()
    {
        PlayerControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered Trigger tagged " + other.gameObject.tag);
        PickUpCollectible(other);
    }

    private void PlayerControl()
    {
        var jump = Input.GetAxis("Jump");
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (jump > 0 && _rb.position.y < maxJumpHeight)
        {
            movement.y = 2.0f;
        }

        _rb.AddForce(movement * speed);
    }

    private void PickUpCollectible(Component other)
    {
        if (!other.gameObject.CompareTag("Pick Up")) return;
        other.gameObject.SetActive(false);
        _scoresCount += 1;
        SetScoresText();
    }

    private void SetScoresText()
    {
        scoresText.text = "Scores: " + _scoresCount;
    }
}