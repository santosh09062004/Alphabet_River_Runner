using UnityEngine;

public class CoinRotateFloat : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 200f;

    [Header("Floating")]
    [SerializeField] private float floatHeight = 0.15f;
    [SerializeField] private float floatSpeed = 2f;

    private Vector3 startLocalPosition;
    private float phaseOffset;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
        phaseOffset = Random.Range(0f, 2f); // prevents sync
    }

    private void Update()
    {
        // Rotation
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        // Floating relative to lotus
        float offset = Mathf.Sin((Time.time + phaseOffset) * floatSpeed) * floatHeight;
        transform.localPosition = startLocalPosition + Vector3.up * offset;
    }
}