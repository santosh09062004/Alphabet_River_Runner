using UnityEngine;
using System.Collections.Generic;

public class LotusSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject lotusPrefab;
    public GameObject[] letterPrefabs; // 26 letters A–Z
    public AlphabetManager alphabetManager;

    [Header("Grid Settings")]
    public float laneDistance = 2.5f;
    public float forwardStep = 2.5f;

    private float nextSpawnZ;

    private GameObject currentStandingLotus;
    private List<GameObject> currentChoices = new List<GameObject>();

    void Start()
    {
        SpawnStartingLotus();

        nextSpawnZ = forwardStep;
        SpawnChoiceRow();
    }

    void SpawnStartingLotus()
    {
        Vector3 startPos = new Vector3(0f, 0f, 0f);
        currentStandingLotus = Instantiate(lotusPrefab, startPos, Quaternion.identity);
    }

    void SpawnChoiceRow()
    {
        currentChoices.Clear();
        if (alphabetManager.IsGameWon())
        return;
        // Determine player's current lane
        int playerLane = GetLaneFromPosition(currentStandingLotus.transform.position.x);

        // Only allow reachable lanes
        List<int> possibleLanes = new List<int>();
        possibleLanes.Add(playerLane);

        if (playerLane - 1 >= 0)
            possibleLanes.Add(playerLane - 1);

        if (playerLane + 1 <= 2)
            possibleLanes.Add(playerLane + 1);

        int correctLane = possibleLanes[Random.Range(0, possibleLanes.Count)];

        char targetLetter = alphabetManager.GetCurrentTarget();
        int targetIndex = targetLetter - 'A';

        for (int lane = 0; lane < 3; lane++)
        {
            float xPos = (lane - 1) * laneDistance;
            Vector3 spawnPos = new Vector3(xPos, 0f, nextSpawnZ);

            GameObject lotus = Instantiate(lotusPrefab, spawnPos, Quaternion.identity);
            LotusLetter lotusLetter = lotus.GetComponent<LotusLetter>();

            if (lane == correctLane)
            {
                lotusLetter.SetLetter(targetLetter, letterPrefabs[targetIndex]);
            }
            else
            {
                int randomIndex;

                do
                {
                    randomIndex = Random.Range(0, 26);
                }
                while (randomIndex == targetIndex);

                char randomChar = (char)('A' + randomIndex);
                lotusLetter.SetLetter(randomChar, letterPrefabs[randomIndex]);
            }

            currentChoices.Add(lotus);
        }

        nextSpawnZ += forwardStep;
    }

    public LotusLetter GetCurrentLotusLetter(int laneIndex)
    {
        return currentChoices[laneIndex].GetComponent<LotusLetter>();
    }

    public void OnPlayerLanded(int chosenLane)
    {
        if (currentStandingLotus != null)
            Destroy(currentStandingLotus);

        currentStandingLotus = currentChoices[chosenLane];

        for (int i = 0; i < currentChoices.Count; i++)
        {
            if (i != chosenLane && currentChoices[i] != null)
                Destroy(currentChoices[i]);
        }

        if (!alphabetManager.IsGameWon())
        {
            SpawnChoiceRow();
        }

        
    }

    int GetLaneFromPosition(float xPos)
    {
        if (xPos < -0.1f) return 0;
        if (xPos > 0.1f) return 2;
        return 1;
    }
}