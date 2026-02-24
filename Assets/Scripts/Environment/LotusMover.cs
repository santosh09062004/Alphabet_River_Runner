using UnityEngine;

public class LotusMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    private int laneIndex;
    private LotusSpawner spawner;

    private bool isFrozen = false;

    public void Initialize(int lane, LotusSpawner laneSpawner)
    {
        laneIndex = lane;
        spawner = laneSpawner;
    }

    public void FreezeLotus()
    {
        isFrozen = true;
    }

    public void UnfreezeLotus()
    {
        isFrozen = false;
    }

    void FixedUpdate()
    {
        if (!LotusGameController.gameStarted) return;

        if (!isFrozen)
        {
            transform.position += Vector3.back * moveSpeed * Time.fixedDeltaTime;
        }

        if (transform.position.z < -10f)
        {
            if (spawner != null)
                spawner.LotusPassed(laneIndex);

            Destroy(gameObject);
        }
    }
}