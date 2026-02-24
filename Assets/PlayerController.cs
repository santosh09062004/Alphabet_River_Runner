using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1; // 0 = Left, 1 = Center, 2 = Right
    private float[] lanePositions = { -3f, 0f, 3f };

    public float jumpForce = 7f;
    public float laneSwitchSpeed = 12f;

    private Rigidbody rb;
    private PlayerInputActions inputActions;

    private bool isGrounded = true;
    private bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!isGrounded || isJumping)
            return;

        // Detect which key was pressed
        if (context.control.name == "leftArrow")
        {
            MoveLeft();
        }
        else if (context.control.name == "rightArrow")
        {
            MoveRight();
        }
    }

    private void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            JumpToLane();
        }
    }

    private void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            JumpToLane();
        }
    }

    private void JumpToLane()
    {
        isGrounded = false;
        isJumping = true;

        Vector3 targetPosition = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z
        );

        // Reset vertical velocity before jump
        rb.velocity = new Vector3(0, 0, 0);

        // Apply upward force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        StartCoroutine(MoveHorizontally(targetPosition));
    }

    private System.Collections.IEnumerator MoveHorizontally(Vector3 target)
    {
        while (Mathf.Abs(transform.position.x - target.x) > 0.05f)
        {
            float step = laneSwitchSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(target.x, transform.position.y, transform.position.z),
                step
            );

            yield return null;
        }

        isJumping = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lotus"))
        {
            isGrounded = true;
        }
    }
}