using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager manager = FindObjectOfType<CoinManager>();
            manager.AddCoin();

            AudioManager.Instance?.PlayCoin();

            Destroy(gameObject);
        }
    }
}