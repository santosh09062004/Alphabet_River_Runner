using UnityEngine;

public class LotusSpawner : MonoBehaviour
{
    public GameObject lotusPrefab;
    public float spawnInterval = 2f;      // gap between lotuses
    public float spawnZ = 30f;            // how far ahead to spawn
    public float speed = 5f;

    float[] laneX = { -3f, 0f, 3f };     // 3 lane positions
    float timer;

    void Start()
    {
        // Spawn initial lotuses so player has something to land on
        SpawnRow();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnRow();
        }
    }

    void SpawnRow()
    {
        foreach (float x in laneX)
        {
            Vector3 spawnPos = new Vector3(x, 0.05f, spawnZ);
            Instantiate(lotusPrefab, spawnPos, Quaternion.identity);
        }
    }
}