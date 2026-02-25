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

        letterVisual = Instantiate(letterPrefab, spawnPoint.position, Quaternion.Euler(0f, 180f, 0f));

        if (newLetter == 'J' || newLetter == 'Q')
        {
            letterVisual.transform.position += Vector3.up * 0.3f;
        }

        letterVisual.transform.SetParent(spawnPoint);
    }

    public char GetLetter()
    {
        return letter;
    }
}