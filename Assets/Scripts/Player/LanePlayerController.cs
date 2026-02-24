using UnityEngine;
using UnityEngine.InputSystem;

public class LanePlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float forwardForce = 5f;

    private bool isGrounded = false;

    private int currentLane = 1;
    private float[] lanePositions = new float[3] { -2f, 0f, 2f };

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // This function name MUST match Input Action name
    public void OnMove(InputValue value)
    {
        if (!isGrounded) return;

        Vector2 moveInput = value.Get<Vector2>();

        if (moveInput.x < 0)
        {
            if (currentLane > 0)
            {
                currentLane--;
                JumpToLane();
            }
        }
        else if (moveInput.x > 0)
        {
            if (currentLane < 2)
            {
                currentLane++;
                JumpToLane();
            }
        }
    }

    void JumpToLane()
    {
        isGrounded = false;

        Vector3 targetPosition = new Vector3(
            lanePositions[currentLane],
            transform.position.y,
            transform.position.z + 2f
        );

        Vector3 direction = (targetPosition - transform.position).normalized;

        rb.velocity = Vector3.zero;

        rb.AddForce(
            new Vector3(direction.x * forwardForce, jumpForce, direction.z * forwardForce),
            ForceMode.Impulse
        );
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lotus"))
        {
            isGrounded = true;

            LotusMover newLotus = collision.gameObject.GetComponent<LotusMover>();

            LotusGameController.gameStarted = true;

            if (LotusGameController.currentPlayerLotus != null)
            {
                LotusGameController.currentPlayerLotus.UnfreezeLotus();
            }

            newLotus.FreezeLotus();
            LotusGameController.currentPlayerLotus = newLotus;
        }
    }
}