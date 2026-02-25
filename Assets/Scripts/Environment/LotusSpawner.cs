using UnityEngine;
using System.Collections.Generic;

public class LotusSpawner : MonoBehaviour
{
    public GameObject lotusPrefab;
    public GameObject[] letterPrefabs;
    public AlphabetManager alphabetManager;

    public float laneDistance = 2.5f;
    public float forwardStep = 2.5f;

    private float nextSpawnZ;
    private GameObject currentStandingLotus;
    private List<GameObject> currentChoices = new List<GameObject>();

    void Start()
    {
        currentStandingLotus = Instantiate(lotusPrefab, Vector3.zero, Quaternion.identity);
        nextSpawnZ = forwardStep;

        SpawnChoiceRow(true); // First row must contain A
    }

    void SpawnChoiceRow(bool forceTarget = false)
    {
        if (alphabetManager.IsGameWon())
            return;

        currentChoices.Clear();

        int playerLane = GetLaneFromPosition(currentStandingLotus.transform.position.x);

        List<int> reachable = new List<int> { playerLane };
        if (playerLane - 1 >= 0) reachable.Add(playerLane - 1);
        if (playerLane + 1 <= 2) reachable.Add(playerLane + 1);

        char targetLetter = alphabetManager.GetCurrentTarget();
        int targetIndex = targetLetter - 'A';

        int correctLane = reachable[Random.Range(0, reachable.Count)];

        for (int lane = 0; lane < 3; lane++)
        {
            float x = (lane - 1) * laneDistance;
            Vector3 pos = new Vector3(x, 0f, nextSpawnZ);

            GameObject lotus = Instantiate(lotusPrefab, pos, Quaternion.identity);
            LotusLetter lotusLetter = lotus.GetComponent<LotusLetter>();

            if (lane == correctLane)
            {
                lotusLetter.SetLetter(targetLetter, letterPrefabs[targetIndex]);
            }
            else
            {
                int rand;
                do
                {
                    rand = Random.Range(0, 26);
                }
                while (rand == targetIndex);

                lotusLetter.SetLetter((char)('A' + rand), letterPrefabs[rand]);
            }

            currentChoices.Add(lotus);
        }

        nextSpawnZ += forwardStep;
    }

    public GameObject GetCurrentLotusObject(int laneIndex)
    {
        return currentChoices[laneIndex];
    }

    public void OnPlayerLanded(int chosenLane)
    {
        if (currentStandingLotus != null)
            Destroy(currentStandingLotus);

        GameObject chosen = currentChoices[chosenLane];
        currentStandingLotus = chosen;

        for (int i = 0; i < currentChoices.Count; i++)
            if (i != chosenLane)
                Destroy(currentChoices[i]);

        SpawnChoiceRow();
    }

    int GetLaneFromPosition(float x)
    {
        if (x < -0.1f) return 0;
        if (x > 0.1f) return 2;
        return 1;
    }
}