using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxJumpHeight;
    public Text scoresText;
    public InputActionMap ballActions;

    private Rigidbody _rb;
    private Vector3 _movement;
    private Vector2 _actionValueMove;
    private bool _actionValueJump;
    private float _initialPlayerVerticalPosition;
    private int _scoresCount;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _initialPlayerVerticalPosition = _rb.position.y;
        _scoresCount = 0;
        SetScoresText();
    }

    private void OnEnable()
    {
        ballActions.Enable();
        ballActions.FindAction("Move").performed += HandleMove;
        ballActions.FindAction("Move").canceled += HandleCancelMove;
        ballActions.FindAction("Jump").performed += HandleJump;
        ballActions.FindAction("Jump").canceled += HandleCancelJump;
    }

    private void OnDisable()
    {
        ballActions.FindAction("Move").performed -= HandleMove;
        ballActions.FindAction("Move").canceled -= HandleCancelMove;
        ballActions.FindAction("Jump").performed -= HandleJump;
        ballActions.FindAction("Jump").canceled -= HandleCancelJump;
        ballActions.Disable();
    }

    private void FixedUpdate()
    {
        PlayerControl();
        Debug.Log(_movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entered Trigger tagged " + other.gameObject.tag);
        PickUpCollectible(other);
    }


    private void HandleMove(InputAction.CallbackContext context)
    {
        _actionValueMove = context.ReadValue<Vector2>();
    }

    private void HandleCancelMove(InputAction.CallbackContext context)
    {
        _actionValueMove = context.ReadValue<Vector2>();
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        _actionValueJump = true;
    }

    private void HandleCancelJump(InputAction.CallbackContext context)
    {
        _actionValueJump = false;
    }

    private void PlayerControl()
    {
        PlayerMove();
        PlayerJump();
        _rb.AddForce(_movement * speed);
    }

    private void PlayerMove()
    {
        _movement = new Vector3(_actionValueMove.x, 0.0f, _actionValueMove.y);
    }

    private void PlayerJump()
    {
        if (_actionValueJump)
        {
            if (_rb.position.y < maxJumpHeight)
            {
                _movement.y = 3.0f;
            }
        } else {
            if (_rb.position.y > _initialPlayerVerticalPosition)
            {
                _movement.y = -1.0f;
            }
        }
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