using UnityEngine;

public class LotusSpawner : MonoBehaviour
{
    public GameObject lotusPrefab;

    public float startZ = 0f;
    public float spacing = 12f;

    private float[] lanePositions = new float[3] { -2f, 0f, 2f };
    private float[] laneNextSpawnZ = new float[3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            laneNextSpawnZ[i] = startZ;
            SpawnLotus(i);
        }
    }

    void SpawnLotus(int lane)
    {
        Vector3 spawnPos = new Vector3(
            lanePositions[lane],
            0.6f, // Raised above river
            laneNextSpawnZ[lane]
        );

        GameObject lotus = Instantiate(lotusPrefab, spawnPos, Quaternion.identity);

        LotusMover mover = lotus.GetComponent<LotusMover>();
        mover.Initialize(lane, this);

        laneNextSpawnZ[lane] += spacing;
    }

    public void LotusPassed(int lane)
    {
        SpawnLotus(lane);
    }
}