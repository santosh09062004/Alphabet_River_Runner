using UnityEngine;

public class AlphabetManager : MonoBehaviour
{
    private char currentTarget = 'A';
    private bool gameOver = false;
    private bool gameWon = false;

    public char GetCurrentTarget() => currentTarget;
    public bool IsGameOver() => gameOver;
    public bool IsGameWon() => gameWon;

    public bool CheckLetter(char selectedLetter)
    {
        if (gameOver || gameWon)
            return false;

        if (selectedLetter == currentTarget)
        {
            AudioManager.Instance?.PlayCorrect();

            if (currentTarget == 'Z')
                gameWon = true;
            else
                currentTarget++;

            return true;
        }
        else
        {
            AudioManager.Instance?.PlayWrong();
            gameOver = true;
            return false;
        }
    }
}