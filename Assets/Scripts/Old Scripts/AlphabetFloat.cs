using UnityEngine;

public class AlphabetFloat : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.12f;
    [SerializeField] private float floatSpeed = 2.5f;

    private Vector3 startLocalPosition;
    private float phaseOffset;

    private void Start()
    {
        startLocalPosition = transform.localPosition;
        phaseOffset = Random.Range(0f, 2f);
    }

    private void Update()
    {
        float offset = Mathf.Sin((Time.time + phaseOffset) * floatSpeed) * floatHeight;
        transform.localPosition = startLocalPosition + Vector3.up * offset;
    }
}