using UnityEngine;

public class LotusLetter : MonoBehaviour
{
    private char letter;
    private GameObject letterVisual;

    public void SetLetter(char newLetter, GameObject letterPrefab)
    {
        letter = newLetter;

        if (letterVisual != null)
            Destroy(letterVisual);

        Transform spawnPoint = transform.Find("LetterSpawnPoint");

        if (spawnPoint == null)
        {
            Debug.LogError("LetterSpawnPoint not found in Lotus prefab.");
            return;
        }

        letterVisual = Instantiate(
            letterPrefab,
            spawnPoint.position,
            Quaternion.Euler(0f, 180f, 0f)
        );

        letterVisual.transform.SetParent(spawnPoint);

        // Adjust J and Q height if needed
        if (newLetter == 'J' || newLetter == 'Q')
        {
            letterVisual.transform.position += Vector3.up * 0.3f;
        }
    }

    public char GetLetter()
    {
        return letter;
    }

    public void RemoveLetterVisual()
    {
        if (letterVisual != null)
        {
            Destroy(letterVisual);
        }
    }
}