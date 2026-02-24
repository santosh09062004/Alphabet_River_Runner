using UnityEngine;

public class LotusMover : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    // Auto destroy when it passes behind the camera
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}