using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerJumpTeleport : MonoBehaviour
{
    [Header("References")]
    public LotusSpawner lotusSpawner;
    public AlphabetManager alphabetManager;

    [Header("Grid Settings")]
    public float laneDistance = 2.5f;
    public float forwardStep = 2.5f;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float jumpDuration = 0.3f;

    private int currentLane = 1;
    private float currentZ = 0f;

    private bool isJumping = false;

    void Update()
    {
        if (isJumping) return;
        if (alphabetManager.IsGameOver() || alphabetManager.IsGameWon()) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            TryShiftLane(-1);
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            TryShiftLane(1);
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            MoveForward();
        }
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

            Vector3 move = Vector3.Lerp(start, target, t);
            move.y += height;

            transform.position = move;

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isJumping = false;

        LotusLetter lotusLetter = lotusSpawner.GetCurrentLotusLetter(currentLane);
        char landedLetter = lotusLetter.GetLetter();

        bool correct = alphabetManager.CheckLetter(landedLetter);

        if (correct)
        {
            lotusSpawner.OnPlayerLanded(currentLane);
        }
    }
}