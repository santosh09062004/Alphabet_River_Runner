using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerJumpTeleport : MonoBehaviour
{
    [Header("References")]
    public LotusSpawner lotusSpawner;
    public AlphabetManager alphabetManager;

    [Header("Lane Settings")]
    public float laneDistance = 2.5f;
    public float forwardStep = 2.5f;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float jumpDuration = 0.4f;

    [Header("River Settings")]
    public float riverYLevel = -2f;
    public float fallDuration = 0.8f;

    private int currentLane = 1;
    private float currentZ = 0f;

    private bool isJumping = false;
    private bool hasFallen = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isJumping || hasFallen) return;
        if (alphabetManager.IsGameOver() || alphabetManager.IsGameWon()) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            TryShiftLane(-1);

        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            TryShiftLane(1);

        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            MoveForward();
    }

    void TryShiftLane(int direction)
    {
        int targetLane = currentLane + direction;

        if (targetLane < 0 || targetLane > 2)
            return;

        currentLane = targetLane;
        PerformJump();
    }

    void MoveForward()
    {
        PerformJump();
    }

    void PerformJump()
    {
        if (animator != null)
            animator.SetTrigger("JumpTrigger");

        currentZ += forwardStep;

        Vector3 targetPosition = new Vector3(
            (currentLane - 1) * laneDistance,
            transform.position.y,
            currentZ
        );

        StartCoroutine(JumpToPosition(targetPosition));
    }

    IEnumerator JumpToPosition(Vector3 target)
    {
        isJumping = true;

        Vector3 start = transform.position;
        float time = 0f;

        while (time < jumpDuration)
        {
            float t = time / jumpDuration;

            float height = 4f * jumpHeight * t * (1f - t);

            Vector3 pos = Vector3.Lerp(start, target, t);
            pos.y += height;

            transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isJumping = false;

        HandleLanding();
    }

    void HandleLanding()
    {
        GameObject landedLotus = lotusSpawner.GetCurrentLotusObject(currentLane);

        LotusLetter lotusLetter = landedLotus.GetComponent<LotusLetter>();
        char landedLetter = lotusLetter.GetLetter();

        bool correct = alphabetManager.CheckLetter(landedLetter);

        if (correct)
        {
            // Remove letter visually
            lotusLetter.RemoveLetterVisual();

            lotusSpawner.OnPlayerLanded(currentLane);
        }
        else
        {
            StartCoroutine(FallIntoRiver());
        }
    }

    IEnumerator FallIntoRiver()
    {
        isJumping = true;
        hasFallen = true;

        // Disable collider so nothing blocks falling
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        if (animator != null)
            animator.SetTrigger("FallTrigger");

        yield return new WaitForSeconds(0.2f);

        float startY = transform.position.y;
        float time = 0f;

        while (time < fallDuration)
        {
            float t = time / fallDuration;

            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(startY, riverYLevel, t);
            transform.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        Vector3 finalPos = transform.position;
        finalPos.y = riverYLevel;
        transform.position = finalPos;
    }
}