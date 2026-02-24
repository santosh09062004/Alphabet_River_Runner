using UnityEngine;
using System.Collections.Generic;

public class LotusManager : MonoBehaviour
{
    public GameObject lotusPrefab;
    public float worldSpeed = 5f;
    public float spawnDistance = 8f;
    public float laneDistance = 2f;

    private float[] lanes = { -2f, 0f, 2f };
    private List<GameObject> activeLotus = new List<GameObject>();

    void Start()
    {
        SpawnLotus(1); // Start center
    }

    void Update()
    {
        MoveLotus();
    }

    void MoveLotus()
    {
        for (int i = 0; i < activeLotus.Count; i++)
        {
            activeLotus[i].transform.Translate(Vector3.back * worldSpeed * Time.deltaTime);

            if (activeLotus[i].transform.position.z < -10f)
            {
                Destroy(activeLotus[i]);
                activeLotus.RemoveAt(i);
            }
        }
    }

    public void SpawnLotus(int laneIndex)
    {
        Vector3 spawnPos = new Vector3(
            lanes[laneIndex],
            0,
            spawnDistance
        );

        GameObject newLotus = Instantiate(lotusPrefab, spawnPos, Quaternion.identity);
        activeLotus.Add(newLotus);
    }
}